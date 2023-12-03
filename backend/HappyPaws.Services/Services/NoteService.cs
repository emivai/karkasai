using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note> AddAsync(Note note)
        {
            return await _noteRepository.AddAsync(note);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _noteRepository.DeleteAsync(id);
        }

        public async Task<List<Note>> GetAllAsync(Guid appointmentId)
        {
            return await _noteRepository.GetAllAsync(appointmentId);
        }

        public async Task<Note> GetAsync(Guid id)
        {
            return await _noteRepository.GetAsync(id);
        }

        public async Task<Note> UpdateAsync(Guid id, Note note)
        {
            return await _noteRepository.UpdateAsync(id, note);
        }
    }
}
