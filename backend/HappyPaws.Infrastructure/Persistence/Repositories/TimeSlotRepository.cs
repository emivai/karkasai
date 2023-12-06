using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly DatabaseContext _context;

        public TimeSlotRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> AddAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();

            return timeSlot;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.TimeSlots.FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.TimeSlots.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TimeSlot>> GetAllAsync(Guid? doctorId = null)
        {
            if(doctorId != null)
                return await _context.TimeSlots.Where(t => t.Start > DateTime.UtcNow && t.Available == true && t.UserId == doctorId).ToListAsync();
            return await _context.TimeSlots.Where(t => t.Start > DateTime.UtcNow && t.Available == true).ToListAsync();
        }

        public async Task<TimeSlot> GetAsync(Guid id)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<TimeSlot> UpdateAsync(Guid id, TimeSlot timeSlot)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Start = timeSlot.Start;
                fromDb.End = timeSlot.End;
                fromDb.UserId = timeSlot.UserId;
                fromDb.Available = timeSlot.Available;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
