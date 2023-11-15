using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            return await _petRepository.AddAsync(pet);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _petRepository.DeleteAsync(id);
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet> GetAsync(Guid id)
        {
            return await _petRepository.GetAsync(id);
        }

        public async Task<Pet> UpdateAsync(Guid id, Pet pet)
        {
            return await _petRepository.UpdateAsync(id, pet);
        }
    }
}
