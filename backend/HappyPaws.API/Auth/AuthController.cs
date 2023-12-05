using HappyPaws.API.Contracts.DTOs.UserDTOs;
using HappyPaws.API.Controllers;
using HappyPaws.Application.Extensions;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Auth;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HappyPaws.API.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenManager _tokenManager;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, ITokenManager tokenManager, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenManager = tokenManager;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _userService.GetByEmailAsync(loginUserDTO.Email);

            if (user == null) throw new BadRequestException("Email or password is invalid");

            if (!user.Verify(loginUserDTO.Password)) throw new BadRequestException("Email or password is invalid");

            var accessToken = _tokenManager.CreateAccessToken(user);
            var refreshToken = _tokenManager.CreateRefreshToken();

            await _refreshTokenService.CreateAsync(new RefreshTokenEntity { UserId = user.Id, Token = refreshToken.Token, Expires = refreshToken.Expiration});

            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions{ Expires = refreshToken.Expiration });

            return Ok(new { AccessToken = accessToken});
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CreateUserDTO createUserDTO)
        {
            var user = await _userService.GetByEmailAsync(createUserDTO.Email);

            if (user != null) throw new BadRequestException("User with this email already exists");

            await _userService.AddAsync(CreateUserDTO.ToDomain(createUserDTO));

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("currentUser")]
        public async Task<IActionResult> CurrentUser()
        {
            var bearerToken = Request.Headers["authorization"].ToString().Replace("Bearer ", "");
            var refreshToken = Request.Cookies["refreshToken"];
            var token = _tokenManager.DecodeAccessToken(bearerToken);

            var userId = GetUserId();        

            var dbRefreshToken = await _refreshTokenService.GetAsync(userId);

            if (token == null || token.ValidFrom > DateTime.UtcNow){ return Unauthorized();}

            User? user = await _userService.GetAsync(userId); 

            if (token.ValidTo < DateTime.UtcNow)
            {
                if (dbRefreshToken == null || dbRefreshToken.Token != refreshToken || dbRefreshToken.Expires < DateTime.UtcNow) { return Unauthorized(); }

                bearerToken = _tokenManager.CreateAccessToken(user);
                var newRefreshToken = _tokenManager.CreateRefreshToken();

                SetRefreshToken(newRefreshToken);
                await _refreshTokenService.UpdateAsync(userId, new RefreshTokenEntity { UserId = user.Id, Token = newRefreshToken.Token, Expires = newRefreshToken.Expiration });
            }

            return Ok(new { user = UserDTO.FromDomain(user), token = bearerToken });
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = GetUserId();

            var dbRefreshToken = await _refreshTokenService.GetAsync(userId);

            if (dbRefreshToken != null) await _refreshTokenService.DeleteAsync(userId);

            Response.Cookies.Delete("refreshToken");

            return Ok();
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions { Expires = refreshToken.Expiration, HttpOnly = true });
        }
    }
}
