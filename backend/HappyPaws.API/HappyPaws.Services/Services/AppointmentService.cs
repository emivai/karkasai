using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, ITimeSlotRepository timeSlotRepository)
        {
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await ClaimTimeSlot(appointment);

            return await _appointmentRepository.AddAsync(appointment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var appointment = await GetAsync(id);

            await VaccateTimeSlot(appointment);

            await _appointmentRepository.DeleteAsync(id);
        }

        public async Task<List<Appointment>> GetAllAsync(Guid petId)
        {
            return await _appointmentRepository.GetAllAsync(petId);
        }

        public async Task<Appointment> GetAsync(Guid id)
        {
            return await _appointmentRepository.GetAsync(id);
        }

        public async Task<Appointment> UpdateAsync(Guid id, Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(id, appointment);
        }

        private async Task ClaimTimeSlot(Appointment appointment)
        {
            var timeSlot = await _timeSlotRepository.GetAsync(appointment.TimeSlotId);

            timeSlot.Available = false;

            await _timeSlotRepository.UpdateAsync(appointment.TimeSlotId, timeSlot);
        }

        private async Task VaccateTimeSlot(Appointment appointment)
        {
            var timeSlot = await _timeSlotRepository.GetAsync(appointment.TimeSlotId);

            timeSlot.Available = true;

            await _timeSlotRepository.UpdateAsync(appointment.TimeSlotId, timeSlot);
        }
    }
}
