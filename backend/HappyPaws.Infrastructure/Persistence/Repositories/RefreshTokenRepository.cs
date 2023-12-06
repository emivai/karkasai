using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DatabaseContext _context;

        public RefreshTokenRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(RefreshTokenEntity refreshToken)
        {
            var existing = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.UserId == refreshToken.UserId);

            if (existing != null) { await UpdateAsync(refreshToken.UserId, refreshToken); }
            else { _context.RefreshTokens.Add(refreshToken); await _context.SaveChangesAsync(); }
        }

        public async Task DeleteAsync(Guid userId)
        {
            var fromDb = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.UserId == userId);

            if (fromDb is null) return;

            _context.RefreshTokens.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshTokenEntity> GetAsync(Guid userId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateAsync(Guid userId, RefreshTokenEntity refreshToken)
        {
            var fromDb = await GetAsync(userId);

            if (fromDb != null)
            {
                fromDb.Token = refreshToken.Token;
                fromDb.Expires = refreshToken.Expires;
            }

            await _context.SaveChangesAsync();
        }
    }
}
