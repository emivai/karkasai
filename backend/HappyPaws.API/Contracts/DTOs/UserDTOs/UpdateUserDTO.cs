using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Photo { get; set; }

        public static User ToDomain(UpdateUserDTO userDTO)
        {
            return new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                Photo = userDTO.Photo
            };
        }
    }
}
