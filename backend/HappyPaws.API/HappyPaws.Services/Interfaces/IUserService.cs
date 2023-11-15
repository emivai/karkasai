using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IUserService
    {
        public Task<User> AddAsync(User user);
        public Task<User> GetAsync(Guid id);
        public Task<List<User>> GetAllAsync();
        public Task<User> UpdateAsync(Guid id, User user);
        public Task DeleteAsync(Guid id);
    }
}
