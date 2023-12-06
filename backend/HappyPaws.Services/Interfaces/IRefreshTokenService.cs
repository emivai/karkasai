using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenEntity> GetAsync(Guid userId);
        Task CreateAsync(RefreshTokenEntity refreshToken);
        Task UpdateAsync(Guid userId, RefreshTokenEntity refreshToken);
        Task DeleteAsync(Guid userId);
    }
}
