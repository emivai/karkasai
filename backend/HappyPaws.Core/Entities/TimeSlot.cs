using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class TimeSlot : Entity, IUserOwnedResource
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Available { get; set; }
        public Guid UserId { get; set; }
        public User Doctor { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
