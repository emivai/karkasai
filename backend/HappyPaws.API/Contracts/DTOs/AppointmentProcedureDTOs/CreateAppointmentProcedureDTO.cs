using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs
{
    public class CreateAppointmentProcedureDTO
    {
        public Guid ProcedureId { get; set; }

        public static AppointmentProcedure ToDomain(CreateAppointmentProcedureDTO appointmentProcedureDTO, Guid appointmentId, Guid userId)
        {
            return new AppointmentProcedure
            {
                ProcedureId = appointmentProcedureDTO.ProcedureId,
                AppointmentId = appointmentId,
                UserId = userId
            };
        }
    }
}
