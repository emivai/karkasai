using HappyPaws.Core.Auth;
using HappyPaws.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace HappyPaws.Application.Interfaces
{
    public interface ITokenManager
    {
        string CreateAccessToken(User user);
        RefreshToken CreateRefreshToken();
        RefreshToken InvalidateRefreshToken(RefreshToken token);
        JwtSecurityToken? DecodeAccessToken(string token);
    }
}
