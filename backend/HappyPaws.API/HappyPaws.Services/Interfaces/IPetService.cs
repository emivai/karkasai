using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IPetService
    {
        public Task<Pet> AddAsync(Pet user);
        public Task<Pet> GetAsync(Guid id);
        public Task<List<Pet>> GetAllAsync();
        public Task<Pet> UpdateAsync(Guid id, Pet pet);
        public Task DeleteAsync(Guid id);
    }
}
