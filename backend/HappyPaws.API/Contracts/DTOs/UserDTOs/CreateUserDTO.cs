using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.UserDTOs
{
    public class CreateUserDTO
    {
        public UserType? Type { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }

        public static User ToDomain(CreateUserDTO userDTO)
        {
            return new User
            {
                Type = (UserType)userDTO.Type,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                Description = userDTO.Description,
                Photo = userDTO.Photo,
                Password = userDTO.Password
            };
        }
    }
}
