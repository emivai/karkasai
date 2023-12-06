using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class AppointmentProcedure : Entity, IUserOwnedResource
    {
        public Guid ProcedureId { get; set; }
        public Guid AppointmentId { get; set; }
        public Procedure Procedure { get; set; }
        public Appointment Appointment { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
