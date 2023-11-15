using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.PetDTOs
{
    public class CreatePetDTO
    {
        public AnimalType? Type { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Photo { get; set; }
        public Guid OwnerId { get; set; }

        public static Pet ToDomain(CreatePetDTO petDTO)
        {
            return new Pet
            {
                Type = (AnimalType)petDTO.Type,
                Name = petDTO.Name,
                Birthdate = petDTO.Birthdate,
                Photo = petDTO.Photo,
                OwnerId = petDTO.OwnerId
            };
        }
    }
}
