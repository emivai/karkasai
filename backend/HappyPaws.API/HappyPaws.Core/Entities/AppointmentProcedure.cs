using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class AppointmentProcedure : Entity
    {
        public Guid ProcedureId { get; set; }
        public Guid AppointmentId { get; set; }
        public Procedure Procedure { get; set; }
        public Appointment Appointment { get; set; }
    }
}
