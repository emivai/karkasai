using HappyPaws.Core.Entities.Common;
using HappyPaws.Core.Enums;

namespace HappyPaws.Core.Entities
{
    public class User : Entity
    {
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Pet>? Pets { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public List<TimeSlot>? TimeSlots { get; set; }
    }
}
