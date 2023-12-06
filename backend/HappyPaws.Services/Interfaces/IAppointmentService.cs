using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IAppointmentService
    {
        public Task<Appointment> AddAsync(Appointment appointment);
        public Task<Appointment> GetAsync(Guid id);
        public Task<List<Appointment>> GetAllAsync(Guid petId);
        public Task<List<Appointment>> GetAllForDoctorAsync(Guid petId, Guid doctorId);
        public Task<Appointment> UpdateAsync(Guid id, Appointment appointment);
        public Task DeleteAsync(Guid id);
    }
}
