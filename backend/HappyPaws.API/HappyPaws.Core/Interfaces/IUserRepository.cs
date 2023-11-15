using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> AddAsync(User user);
        public Task<User> GetAsync(Guid id);
        public Task<List<User>> GetAllAsync(UserType? type);
        public Task<User> UpdateAsync(Guid id, User user);
        public Task DeleteAsync(Guid id);
    }
}
