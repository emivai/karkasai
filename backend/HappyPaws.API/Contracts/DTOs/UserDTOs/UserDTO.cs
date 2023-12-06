using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Photo { get; set; }

        public static UserDTO FromDomain(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Type = user.Type,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo
            };
        }
    }
}
