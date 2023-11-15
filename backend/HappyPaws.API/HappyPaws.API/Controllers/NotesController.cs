using HappyPaws.API.Contracts.DTOs.NoteDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPetService _petService;

        public NotesController(INoteService noteService, IAppointmentService appointmentService, IPetService petService)
        {
            _noteService = noteService;
            _appointmentService = appointmentService;
            _petService = petService;
        }

        [Route("Pets/{petId}/Appointments/{appointmentId}/[controller]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(Guid petId, Guid appointmentId)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var notes = await _noteService.GetAllAsync(appointmentId);

            var result = notes.Select(NoteDTO.FromDomain).ToList();

            return Ok(result);
        }

        [Route("Pets/{petId}/Appointments/{appointmentId}/[controller]/{noteId}")]
        [HttpGet]
        [ProducesResponseType(typeof(NoteDTO), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(Guid petId, Guid appointmentId, Guid noteId)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var note = await _noteService.GetAsync(noteId) ?? throw new ResourceNotFoundException(); 

            if (note == null || note.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            return Ok(NoteDTO.FromDomain(note));
        }

        [Route("Pets/{petId}/Appointments/{appointmentId}/[controller]")]
        [HttpPost]
        [ProducesResponseType(typeof(NoteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Guid petId, Guid appointmentId, CreateNoteDTO noteDTO)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var created = await _noteService.AddAsync(CreateNoteDTO.ToDomain(noteDTO, appointmentId));

            return StatusCode(StatusCodes.Status201Created, NoteDTO.FromDomain(created));
        }

        [HttpPut("Pets/{petId}/Appointments/{appointmentId}/[controller]/{noteId}")]
        [ProducesResponseType(typeof(NoteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid petId, Guid appointmentId, Guid noteId, UpdateNoteDTO noteDTO)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var note = await _noteService.GetAsync(noteId);

            if (note == null || note.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            var updated = await _noteService.UpdateAsync(noteId, UpdateNoteDTO.ToDomain(noteDTO));

            return Ok(NoteDTO.FromDomain(updated));
        }

        [Route("Pets/{petId}/Appointments/{appointmentId}/[controller]/{noteId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid petId, Guid appointmentId, Guid noteId)
        {
            var pet = await _petService.GetAsync(petId) ?? throw new ResourceNotFoundException();

            var appointment = await _appointmentService.GetAsync(appointmentId);

            if (appointment == null || appointment.PetId != petId) throw new ResourceNotFoundException();

            var note = await _noteService.GetAsync(noteId);

            if (note == null || note.AppointmentId != appointmentId) throw new ResourceNotFoundException();

            await _noteService.DeleteAsync(noteId);

            return NoContent();
        }
    }
}
