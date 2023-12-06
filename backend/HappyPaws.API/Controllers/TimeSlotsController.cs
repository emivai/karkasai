using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.TimeSlotDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public TimeSlotsController(ITimeSlotService timeSlotService, IUserService userService, IAuthorizationService authorizationService)
        {
            _timeSlotService = timeSlotService;
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TimeSlotDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var timeSlots = await _timeSlotService.GetAllAsync();

            var result = timeSlots.Select(TimeSlotDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TimeSlotDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var timeSlot = await _timeSlotService.GetAsync(id);

            if (timeSlot == null) throw new ResourceNotFoundException();

            return Ok(TimeSlotDTO.FromDomain(timeSlot));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TimeSlotDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateTimeSlotDTO timeSlotDTO)
        {
            var userId = new Guid(User.FindFirst("UserId").Value);

            var doctor = await _userService.GetAsync(userId);

            if (doctor == null) throw new ResourceNotFoundException();

            if (doctor.Type != UserType.Doctor) throw new UserTypeException(UserType.Doctor);

            var created = await _timeSlotService.AddAsync(CreateTimeSlotDTO.ToDomain(timeSlotDTO, userId));

            return StatusCode(StatusCodes.Status201Created, TimeSlotDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TimeSlotDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateTimeSlotDTO timeSlotDTO)
        {
            var userId = new Guid(User.FindFirst("UserId").Value);

            var timeSlot = _timeSlotService.GetAsync(id);

            var doctor = await _userService.GetAsync(userId);

            if (doctor == null) throw new ResourceNotFoundException();

            if (doctor.Type != UserType.Doctor) throw new UserTypeException(UserType.Doctor);

            if (timeSlot == null) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, timeSlot, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var updated = await _timeSlotService.UpdateAsync(id, UpdateTimeSlotDTO.ToDomain(timeSlotDTO, userId));

            return Ok(TimeSlotDTO.FromDomain(updated));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var timeSlot = _timeSlotService.GetAsync(id);

            if (timeSlot == null) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, timeSlot, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _timeSlotService.DeleteAsync(id);

            return NoContent();
        }
    }
}
