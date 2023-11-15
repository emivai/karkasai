using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.Users.FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.Users.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync(UserType? type = null)
        {
            if(type != null)
                return await _context.Users.Where(u => u.Type == type).ToListAsync();

            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Name = user.Name;
                fromDb.Surname = user.Surname;
                fromDb.Email = user.Email;
                fromDb.PhoneNumber = user.PhoneNumber;
                fromDb.Photo = user.Photo ?? fromDb.Photo;
                fromDb.Description = user.Description ?? fromDb.Description;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
