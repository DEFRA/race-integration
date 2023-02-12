using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RACE2.FrontEnd;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
builder.Services.AddScoped(sp => http);

using var configSettings = await http.GetAsync("settings.json");
using var stream = await configSettings.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);
string RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];

builder.Services.AddRACE2GraphQLClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5003/graphql/"));

//builder.Services.AddOidcAuthentication(options =>
//{
//    // Configure your authentication provider options here.
//    // For more information, see https://aka.ms/blazor-standalone-auth
//    builder.Configuration.Bind("oidc", options.ProviderOptions);
//    options.UserOptions.RoleClaim = "role";
//});

await builder.Build().RunAsync();
