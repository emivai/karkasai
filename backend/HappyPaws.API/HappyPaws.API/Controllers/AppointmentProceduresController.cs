using HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AppointmentProceduresController : ControllerBase
    {
        private readonly IAppointmentProcedureService _appointmentProceduresService;
        private readonly IAppointmentService _appointmentsService;
        private readonly IProcedureService _proceduresService;

        public AppointmentProceduresController(IAppointmentProcedureService appointmentProceduresService, 
                                                IAppointmentService appointmentsService, 
                                                IProcedureService proceduresService)
        {
            _appointmentProceduresService = appointmentProceduresService;
            _appointmentsService = appointmentsService;
            _proceduresService = proceduresService;
        }

        [HttpGet("Appointments/{appointmentId}/[controller]")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentProcedureDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid appointmentId)
        {
            var appointment = await _appointmentsService.GetAsync(appointmentId) ?? throw new ResourceNotFoundException();

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

            var procedure = await _proceduresService.GetAsync(appointmentProcedureDTO.ProcedureId) ?? throw new BadRequestException("The specified procedureId does not exist.");

            var created = await _appointmentProceduresService.AddAsync(CreateAppointmentProcedureDTO.ToDomain(appointmentProcedureDTO, appointmentId));

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

            if (appointmentProcedure.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            var procedure = await _proceduresService.GetAsync(appointmentProcedureDTO.ProcedureId) ?? throw new BadRequestException("The specified procedureId does not exist.");

            var updated = await _appointmentProceduresService.UpdateAsync(id, UpdateAppointmentProcedureDTO.ToDomain(appointmentProcedureDTO));

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
