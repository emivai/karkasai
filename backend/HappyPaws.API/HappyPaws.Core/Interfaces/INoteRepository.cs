using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface INoteRepository
    {
        public Task<Note> AddAsync(Note note);
        public Task<Note> GetAsync(Guid id);
        public Task<List<Note>> GetAllAsync(Guid appointmentId);
        public Task<Note> UpdateAsync(Guid id, Note note);
        public Task DeleteAsync(Guid id);
    }
}
