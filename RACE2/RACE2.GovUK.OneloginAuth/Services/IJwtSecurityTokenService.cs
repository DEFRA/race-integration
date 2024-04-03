using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RACE2.GovUK.OneloginAuth.Services
{
    public interface IJwtSecurityTokenService
    {
        string CreateToken(string clientId, string audience, ClaimsIdentity claimsIdentity,
            SigningCredentials signingCredentials);
    }
}