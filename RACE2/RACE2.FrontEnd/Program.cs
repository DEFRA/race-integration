using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RACE2.FrontEnd;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using RACE2.Dto;
using RACE2.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fluxor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClientFrontEndApp = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

using var configSettings = await httpClientFrontEndApp.GetAsync("appSettings.json");

using var stream = await configSettings.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);
string RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("oidc", options.ProviderOptions);
});
builder.Services
    .AddRACE2GraphQLClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(RACE2WebApiURL);
        client.DefaultRequestHeaders.Add("GraphQL-Preflight", "1");
        //client.DefaultRequestHeaders.Authorization =
        //    new AuthenticationHeaderValue("Bearer", "Your Oauth token");
    });

builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

//builder.Services.AddOidcAuthentication(options =>
//{
//    // Configure your authentication provider options here.
//    // For more information, see https://aka.ms/blazor-standalone-auth
//    builder.Configuration.Bind("oidc", options.ProviderOptions);
//    options.UserOptions.RoleClaim = "role";
//});

await builder.Build().RunAsync();
