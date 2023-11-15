using HappyPaws.API.Contracts.DTOs.UserDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersSerevice) 
        {
            _usersService = usersSerevice;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] UserType? type = null)
        {
            var users = await _usersService.GetAllAsync(type);

            var result = users.Select(UserDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null) throw new ResourceNotFoundException();

            return Ok(UserDTO.FromDomain(user));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateUserDTO userDTO)
        {
            var created = await _usersService.AddAsync(CreateUserDTO.ToDomain(userDTO));

            return StatusCode(StatusCodes.Status201Created, UserDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO userDTO)
        {
            var user = _usersService.GetAsync(id);

            if (user == null) throw new ResourceNotFoundException();

            var updated = await _usersService.UpdateAsync(id, UpdateUserDTO.ToDomain(userDTO));

            return Ok(UserDTO.FromDomain(updated));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = _usersService.GetAsync(id);

            if (user == null) throw new ResourceNotFoundException();

            await _usersService.DeleteAsync(id);

            return NoContent();
        }
    }
}
