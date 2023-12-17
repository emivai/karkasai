using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.AppointmentDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentService _appointmentsService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly IPetService _petService;
        private readonly IAuthorizationService _authorizationService;

        public AppointmentsController(IAppointmentService appointmentsService, ITimeSlotService timeSlotService, IPetService petService, IAuthorizationService authorizationService)
        {
            _appointmentsService = appointmentsService;
            _timeSlotService = timeSlotService;
            _petService = petService;
            _authorizationService = authorizationService;
        }

        [HttpGet("Pets/{petId}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsyncByPetId(Guid petId)
        {
            var pet = await _petService.GetAsync(petId);

            var authResult = await _authorizationService.AuthorizeAsync(User, pet, PolicyNames.Owner);

            if (User.IsInRole("Client") && !authResult.Succeeded) return Forbid();

            List<Core.Entities.Appointment> appointments;

            if (User.IsInRole("Doctor")) appointments = await _appointmentsService.GetAllForDoctorAsync(petId, new Guid(User.FindFirst("UserId")?.Value));
            else appointments = await _appointmentsService.GetAllAsync(petId);

            var result = appointments.Select(AppointmentDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("Pets/{petId}/[controller]/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid petId, Guid appointmentId)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            if (appointment.PetId != petId) throw new ResourceNotFoundException();

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(AppointmentDTO.FromDomain(appointment));
        }

        [Route("Pets/{petId}/[controller]")]
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsyncByPetId(Guid petId, CreateAppointmentDTO appointmentDTO)
        {
            var timeSlot = await _timeSlotService.GetAsync(appointmentDTO.TimeSlotId);

            if (timeSlot == null || !timeSlot.Available) throw new BadRequestException("Time slot invalid or unavailable.");

            if (timeSlot.Start < DateTime.UtcNow) throw new BadRequestException("Cannot use timeslot from the past.");

            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, pet, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();    
            }

            var userId = GetUserId();

            var created = await _appointmentsService.AddAsync(CreateAppointmentDTO.ToDomain(appointmentDTO, petId, userId));

            return StatusCode(StatusCodes.Status201Created, AppointmentDTO.FromDomain(created));
        }

        [HttpPut("Pets/{petId}/[controller]/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsyncByPetId(Guid petId, Guid appointmentId, UpdateAppointmentDTO appointmentDTO)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentsService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId ) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            //var timeSlot = await _timeSlotService.GetAsync(appointmentDTO.TimeSlotId);

            //if (timeSlot == null || (appointment.TimeSlotId != timeSlot.Id && !timeSlot.Available)) throw new BadRequestException("Time slot invalid or unavailable.");

            //if (timeSlot.Start < DateTime.UtcNow) throw new BadRequestException("Cannot use timeslot from the past.");

            var userId = GetUserId();

            var updated = await _appointmentsService.UpdateAsync(appointmentId, UpdateAppointmentDTO.ToDomain(appointmentDTO, petId, userId));

            return Ok(AppointmentDTO.FromDomain(updated));
        }

        [Route("Pets/{petId}/[controller]/{appointmentId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsyncByPetId(Guid petId, Guid appointmentId)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentsService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _appointmentsService.DeleteAsync(appointmentId);

            return NoContent();
        }
    }
}
