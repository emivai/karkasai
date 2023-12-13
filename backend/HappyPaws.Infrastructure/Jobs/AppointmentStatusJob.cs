using HappyPaws.Core.Interfaces;
using Quartz;

namespace HappyPaws.Infrastructure.Jobs
{
    public class AppointmentStatusJob : IJob
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentStatusJob(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var appointments = await _appointmentRepository.GetAllOutdatedAsync();

            foreach (var appointment in appointments) 
            {
                appointment.Status = Core.Enums.AppointmentStatus.Done;

                await _appointmentRepository.UpdateAsync(appointment.Id, appointment);
            }
        }
    }
}
