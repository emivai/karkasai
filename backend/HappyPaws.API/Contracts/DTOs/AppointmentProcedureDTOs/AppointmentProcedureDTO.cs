using HappyPaws.API.Contracts.DTOs.ProcedureDTOs;
using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs
{
    public class AppointmentProcedureDTO
    {
        public Guid Id { get; set; }
        public Guid ProcedureId { get; set; }
        public ProcedureDTO? Procedure { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }

        public static AppointmentProcedureDTO FromDomain(AppointmentProcedure appointmentProcedure)
        {
            return new AppointmentProcedureDTO
            {
                Id = appointmentProcedure.Id,
                ProcedureId = appointmentProcedure.ProcedureId,
                AppointmentId = appointmentProcedure.AppointmentId,
                UserId = appointmentProcedure.UserId,
                Procedure = ProcedureDTO.FromDomain(appointmentProcedure.Procedure)
            };
        }
    }
}
