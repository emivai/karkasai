using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface ITimeSlotRepository
    {
        public Task<TimeSlot> AddAsync(TimeSlot timeSlot);
        public Task<TimeSlot> GetAsync(Guid id);
        public Task<List<TimeSlot>> GetAllAsync();
        public Task<TimeSlot> UpdateAsync(Guid id, TimeSlot timeSlot);
        public Task DeleteAsync(Guid id);
    }
}
