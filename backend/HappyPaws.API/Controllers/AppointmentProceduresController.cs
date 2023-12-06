using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class AppointmentProceduresController : BaseController
    {
        private readonly IAppointmentProcedureService _appointmentProceduresService;
        private readonly IAppointmentService _appointmentsService;
        private readonly IProcedureService _proceduresService;
        private readonly IAuthorizationService _authorizationService;

        public AppointmentProceduresController(IAppointmentProcedureService appointmentProceduresService, 
                                                IAppointmentService appointmentsService, 
                                                IProcedureService proceduresService, 
                                                IAuthorizationService authorizationService)
        {
            _appointmentProceduresService = appointmentProceduresService;
            _appointmentsService = appointmentsService;
            _proceduresService = proceduresService;
            _authorizationService = authorizationService;
        }

        [HttpGet("Appointments/{appointmentId}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentProcedureDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid appointmentId)
        {
            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

            var appointmentProcedures = await _appointmentProceduresService.GetAllAsync(appointmentId);

            var result = appointmentProcedures.Select(AppointmentProcedureDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("Appointments/{appointmentId}/[controller]/{id}")]
        [ProducesResponseType(typeof(AppointmentProcedureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid appointmentId, Guid id)
        {
            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

            var appointmentProcedure = await _appointmentProceduresService.GetAsync(id);

            if (appointmentProcedure == null || appointmentProcedure.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            return Ok(AppointmentProcedureDTO.FromDomain(appointmentProcedure));
        }

        [HttpPost("Appointments/{appointmentId}/[controller]")]
        [ProducesResponseType(typeof(AppointmentProcedureDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Guid appointmentId, CreateAppointmentProcedureDTO appointmentProcedureDTO)
        {
            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded)
            {
                return Forbid();
            }

            var procedure = await _proceduresService.GetAsync(appointmentProcedureDTO.ProcedureId) ?? throw new BadRequestException("The specified procedureId does not exist.");

            var userId = GetUserId();

            var created = await _appointmentProceduresService.AddAsync(CreateAppointmentProcedureDTO.ToDomain(appointmentProcedureDTO, appointmentId, userId));

            return StatusCode(StatusCodes.Status201Created, AppointmentProcedureDTO.FromDomain(created));
        }

        [HttpPut("Appointments/{appointmentId}/[controller]/{id}")]
        [ProducesResponseType(typeof(AppointmentProcedureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid appointmentId, Guid id, UpdateAppointmentProcedureDTO appointmentProcedureDTO)
        {
            var appointmentProcedure = await _appointmentProceduresService.GetAsync(id) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded)
            {
                return Forbid();
            }

            if (appointmentProcedure.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            var procedure = await _proceduresService.GetAsync(appointmentProcedureDTO.ProcedureId) ?? throw new BadRequestException("The specified procedureId does not exist.");

            var userId = GetUserId();

            var updated = await _appointmentProceduresService.UpdateAsync(id, UpdateAppointmentProcedureDTO.ToDomain(appointmentProcedureDTO, userId));

            return Ok(AppointmentProcedureDTO.FromDomain(updated));
        }

        [HttpDelete("Appointments/{appointmentId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid appointmentId, Guid id)
        {
            var appointmentProcedure = await _appointmentProceduresService.GetAsync(id) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

            if (appointmentProcedure.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            await _appointmentProceduresService.DeleteAsync(id);

            return NoContent();
        }
    }
}
