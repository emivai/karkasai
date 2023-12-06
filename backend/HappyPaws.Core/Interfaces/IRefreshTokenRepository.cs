using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetAsync(Guid userId);
        Task CreateAsync(RefreshTokenEntity refreshToken);
        Task UpdateAsync(Guid userId, RefreshTokenEntity refreshToken);
        Task DeleteAsync(Guid userId);
    }
}
