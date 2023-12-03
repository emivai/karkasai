using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface ITimeSlotService
    {
        public Task<TimeSlot> AddAsync(TimeSlot timeSlot);
        public Task<TimeSlot> GetAsync(Guid id);
        public Task<List<TimeSlot>> GetAllAsync();
        public Task<TimeSlot> UpdateAsync(Guid id, TimeSlot timeSlot);
        public Task DeleteAsync(Guid id);
    }
}
