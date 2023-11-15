using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface IPetRepository
    {
        public Task<Pet> AddAsync(Pet pet);
        public Task<Pet> GetAsync(Guid id);
        public Task<List<Pet>> GetAllAsync();
        public Task<Pet> UpdateAsync(Guid id, Pet pet);
        public Task DeleteAsync(Guid id);
    }
}
