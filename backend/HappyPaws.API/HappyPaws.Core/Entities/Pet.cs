using HappyPaws.Core.Entities.Common;
using HappyPaws.Core.Enums;

namespace HappyPaws.Core.Entities
{
    public class Pet : Entity
    {
        public AnimalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Photo { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public List<Appointment>? Appointments { get; set; }
    }
}
