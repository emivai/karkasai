using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class Note : Entity
    {
        public string Value { get; set; }
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
