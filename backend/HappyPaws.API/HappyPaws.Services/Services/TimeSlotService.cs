using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<TimeSlot> AddAsync(TimeSlot timeSlot)
        {
            return await _timeSlotRepository.AddAsync(timeSlot);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _timeSlotRepository.DeleteAsync(id);
        }

        public async Task<List<TimeSlot>> GetAllAsync()
        {
            return await _timeSlotRepository.GetAllAsync();
        }

        public async Task<TimeSlot> GetAsync(Guid id)
        {
            return await _timeSlotRepository.GetAsync(id);
        }

        public async Task<TimeSlot> UpdateAsync(Guid id, TimeSlot timeSlot)
        {
            return await _timeSlotRepository.UpdateAsync(id, timeSlot);
        }
    }
}
