using Fluxor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
//IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
string RACE2WebApiURL = builder.Configuration["ApplicationSettings:RACE2WebApiURL"];
string RACE2IDPURL = builder.Configuration["ApplicationSettings:RACE2SecurityProviderURL"];
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(
    OpenIdConnectDefaults.AuthenticationScheme, 
    options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.Authority = RACE2IDPURL;
        options.ClientId = "blazorServer";
        options.ClientSecret = "blazorserver-secret";

        // When set to code, the middleware will use PKCE protection
        options.ResponseType = "code";

        // Save the tokens we receive from the IDP
        options.SaveTokens = true;

        // It's recommended to always get claims from the
        // UserInfoEndpoint during the flow.
        options.GetClaimsFromUserInfoEndpoint = true;    

        options.Scope.Add("race2WebApi");
    });


builder.Services.AddRACE2GraphQLClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(RACE2WebApiURL));

builder.Services.AddFluxor(o => 
{ 
    o.ScanAssemblies(typeof(Program).Assembly); 
    o.UseReduxDevTools(rdt => { rdt.Name = "RACE2 application"; });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
