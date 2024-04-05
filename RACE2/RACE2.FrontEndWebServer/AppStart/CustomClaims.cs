using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using RACE2.GovUK.OneloginAuth.Services;

namespace RACE2.FrontEndWebServer.AppStart;

public class CustomClaims : ICustomClaims
{
    public async Task<IEnumerable<Claim?>> GetClaims(TokenValidatedContext tokenValidatedContext)
    {
        var value = tokenValidatedContext?.Principal?.Identities.First().Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
            ?.Value;
        return new List<Claim>
        {
            new Claim("EmployerAccountId",$"Kris-{value}"),
            new Claim(ClaimTypes.Name,$"Kris Sahoo")
        };
    }
}