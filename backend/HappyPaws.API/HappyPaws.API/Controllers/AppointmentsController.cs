using HappyPaws.API.Contracts.DTOs.AppointmentDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentsService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly IPetService _petService;

        public AppointmentsController(IAppointmentService appointmentsService, ITimeSlotService timeSlotService, IPetService petService)
        {
            _appointmentsService = appointmentsService;
            _timeSlotService = timeSlotService;
            _petService = petService;
        }

        [HttpGet("Pets/{petId}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsyncByPetId(Guid petId)
        {
            var pet = await _petService.GetAsync(petId);

            var appointments = await _appointmentsService.GetAllAsync(petId);

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

            var created = await _appointmentsService.AddAsync(CreateAppointmentDTO.ToDomain(appointmentDTO, petId));

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

            var timeSlot = await _timeSlotService.GetAsync(appointmentDTO.TimeSlotId);

            if (timeSlot == null || (appointment.TimeSlotId != timeSlot.Id && !timeSlot.Available)) throw new BadRequestException("Time slot invalid or unavailable.");

            if (timeSlot.Start < DateTime.UtcNow) throw new BadRequestException("Cannot use timeslot from the past.");

            var updated = await _appointmentsService.UpdateAsync(appointmentId, UpdateAppointmentDTO.ToDomain(appointmentDTO, petId));

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

            await _appointmentsService.DeleteAsync(appointmentId);

            return NoContent();
        }
    }
}
