using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface IAppointmentRepository
    {
        public Task<Appointment> AddAsync(Appointment appointment);
        public Task<Appointment> GetAsync(Guid id);
        public Task<List<Appointment>> GetAllAsync(Guid petId);
        public Task<List<Appointment>> GetAllForDoctorAsync(Guid petId, Guid doctorId);
        public Task<List<Appointment>> GetAllOutdatedAsync();
        public Task<Appointment> UpdateAsync(Guid id, Appointment appointment);
        public Task DeleteAsync(Guid id);
    }
}
