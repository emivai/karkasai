using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.Application.Interfaces
{
    public interface IPetService
    {
        public Task<Pet> AddAsync(Pet user);
        public Task<Pet> GetAsync(Guid id);
        public Task<List<Pet>> GetAllAsync(Guid? userId);
        public Task<List<Pet>> GetAllForDoctorAsync(Guid userId);
        public Task<Pet> UpdateAsync(Guid id, Pet pet);
        public Task DeleteAsync(Guid id);
    }
}
