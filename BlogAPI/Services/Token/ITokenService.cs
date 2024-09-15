using BlogAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BlogAPI.Services.Token
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user);
    }
}
