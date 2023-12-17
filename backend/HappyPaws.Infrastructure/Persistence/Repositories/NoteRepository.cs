using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DatabaseContext _context;

        public NoteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Note> AddAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.Notes.FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.Notes.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllAsync(Guid appointmentId)
        {
            return await _context.Notes.Where(n => n.AppointmentId == appointmentId).ToListAsync();
        }

        public async Task<Note> GetAsync(Guid id)
        {
            return await _context.Notes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Note> UpdateAsync(Guid id, Note note)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Value = note.Value;
                fromDb.UserId = note.UserId;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
