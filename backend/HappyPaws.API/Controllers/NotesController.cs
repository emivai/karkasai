using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.NoteDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class NotesController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPetService _petService;
        private readonly IAuthorizationService _authorizationService;

        public NotesController(INoteService noteService, IAppointmentService appointmentService, IPetService petService, IAuthorizationService authorizationService)
        {
            _noteService = noteService;
            _appointmentService = appointmentService;
            _petService = petService;
            _authorizationService = authorizationService;
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

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

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

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

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

            var ownerAuthResult = await _authorizationService.AuthorizeAsync(User, appointment, PolicyNames.Owner);
            var doctorAuthResult = await _authorizationService.AuthorizeAsync(User, appointment.TimeSlot.UserId, PolicyNames.Owner);

            if (!ownerAuthResult.Succeeded && !doctorAuthResult.Succeeded)
            {
                return Forbid();
            }

            var userId = GetUserId();

            var created = await _noteService.AddAsync(CreateNoteDTO.ToDomain(noteDTO, appointmentId, userId));

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

            var authResult = await _authorizationService.AuthorizeAsync(User, note, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var userId = GetUserId();

            var updated = await _noteService.UpdateAsync(noteId, UpdateNoteDTO.ToDomain(noteDTO, userId));

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

            var authResult = await _authorizationService.AuthorizeAsync(User, note, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _noteService.DeleteAsync(noteId);

            return NoContent();
        }
    }
}
