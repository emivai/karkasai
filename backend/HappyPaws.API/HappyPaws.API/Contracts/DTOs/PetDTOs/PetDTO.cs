using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.PetDTOs
{
    public class PetDTO
    {
        public Guid Id { get; set; }
        public AnimalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Photo { get; set; }
        public Guid OwnerId { get; set; }

        public static PetDTO FromDomain(Pet pet)
        {
            return new PetDTO
            {
                Id = pet.Id,
                Type = pet.Type,
                Name = pet.Name,
                Birthdate = pet.Birthdate,
                Photo = pet.Photo,
                OwnerId = pet.OwnerId
            };
        }
    }
}
