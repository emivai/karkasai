using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Utilities
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task CreateAsync(RefreshTokenEntity refreshToken)
        {
            await _refreshTokenRepository.CreateAsync(refreshToken);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _refreshTokenRepository.DeleteAsync(userId);
        }

        public async Task<RefreshTokenEntity> GetAsync(Guid userId)
        {
            return await _refreshTokenRepository.GetAsync(userId);
        }

        public async Task UpdateAsync(Guid userId, RefreshTokenEntity refreshToken)
        {
            await _refreshTokenRepository.UpdateAsync(userId, refreshToken);
        }
    }
}
