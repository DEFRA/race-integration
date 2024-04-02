using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RACE2.GovUK.OneloginAuth.Configuration;
using RACE2.GovUK.OneloginAuth.Models;
using RACE2.GovUK.OneloginAuth.Services;


namespace RACE2.GovUK.OneloginAuth.AppStart
{
    internal class EmployerStubAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ICustomClaims _customClaims;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployerStubAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, ICustomClaims customClaims, IHttpContextAccessor httpContextAccessor) : base(
            options, logger, encoder, clock)
        {
            _customClaims = customClaims;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(GovUkConstants.StubAuthCookieName))
            {
                return AuthenticateResult.Fail("");
            }

            var authCookieValue = JsonConvert.DeserializeObject<StubAuthUserDetails>(_httpContextAccessor.HttpContext.Request.Cookies[GovUkConstants.StubAuthCookieName]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, authCookieValue.Email),
                new Claim(ClaimTypes.NameIdentifier, authCookieValue.Id),
                new Claim("sub", authCookieValue.Id)
            };


            var identity = new ClaimsIdentity(claims, "Employer-stub");
            var principal = new ClaimsPrincipal(identity);
            
            if (_customClaims != null)
            {
                var additionalClaims = await _customClaims.GetClaims(new TokenValidatedContext(_httpContextAccessor.HttpContext, Scheme, new OpenIdConnectOptions(), principal, new AuthenticationProperties() ));
                claims.AddRange(additionalClaims);
                principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Employer-stub"));
            }            

            var ticket = new AuthenticationTicket(principal, "Employer-stub");

            
            var result = AuthenticateResult.Success(ticket);

            return result;
        }
    }
}