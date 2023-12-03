using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IAppointmentProcedureService
    {
        public Task<AppointmentProcedure> AddAsync(AppointmentProcedure procedure);
        public Task<AppointmentProcedure> GetAsync(Guid id);
        public Task<List<AppointmentProcedure>> GetAllAsync(Guid appointmentId);
        public Task<AppointmentProcedure> UpdateAsync(Guid id, AppointmentProcedure procedure);
        public Task DeleteAsync(Guid id);
    }
}
