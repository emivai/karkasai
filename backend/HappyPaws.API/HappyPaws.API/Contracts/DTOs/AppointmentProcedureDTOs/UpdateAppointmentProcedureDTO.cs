using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs
{
    public class UpdateAppointmentProcedureDTO
    {
        public Guid ProcedureId { get; set; }
        public Guid AppointmentId { get; set; }

        public static AppointmentProcedure ToDomain(UpdateAppointmentProcedureDTO appointmentProcedureDTO)
        {
            return new AppointmentProcedure
            {
                ProcedureId = appointmentProcedureDTO.ProcedureId,
                AppointmentId = appointmentProcedureDTO.AppointmentId
            };
        }
    }
}
