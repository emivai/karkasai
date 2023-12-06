using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.UserDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IAuthorizationService _authorizationService;

        public UsersController(IUserService usersSerevice, IAuthorizationService authorizationService)
        {
            _usersService = usersSerevice;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] UserType? type = null)
        {
            if(User.IsInRole("Client") && type != UserType.Doctor) return Forbid();

            if (User.IsInRole("Doctor") && type != UserType.Doctor && type != UserType.Client) return Forbid();

            var users = await _usersService.GetAllAsync(type);

            var result = users.Select(UserDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _usersService.GetAsync(id);

            var authResult = await _authorizationService.AuthorizeAsync(User, user, PolicyNames.SameUser);

            if(User.IsInRole("Client") && user.Type != UserType.Doctor && !authResult.Succeeded) return Forbid();
            if (User.IsInRole("Doctor") && user.Type == UserType.Admin) return Forbid();

            if (user == null) throw new ResourceNotFoundException();

            return Ok(UserDTO.FromDomain(user));
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO userDTO)
        {
            var user = _usersService.GetAsync(id);

            if (user == null) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, user, PolicyNames.SameUser);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var updated = await _usersService.UpdateAsync(id, UpdateUserDTO.ToDomain(userDTO));

            return Ok(UserDTO.FromDomain(updated));
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, user, PolicyNames.SameUser);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _usersService.DeleteAsync(id);

            return NoContent();
        }
    }
}
