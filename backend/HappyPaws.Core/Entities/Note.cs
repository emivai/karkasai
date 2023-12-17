using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class Note : Entity, IUserOwnedResource
    {
        public string Value { get; set; } = string.Empty;
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
