using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RACE2.GovUK.OneloginAuth.Models;

namespace RACE2.GovUK.OneloginAuth.Services
{
    public interface IOidcService
    {
        Task<Token> GetToken(OpenIdConnectMessage openIdConnectMessage);
        Task PopulateAccountClaims(TokenValidatedContext tokenValidatedContext);
    }
}