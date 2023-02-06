using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RACE2.FrontEnd;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string fileName = "RACE2.FrontEnd.appsettings.json";
var stream = Assembly.GetExecutingAssembly()
                     .GetManifestResourceStream(fileName);

var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
builder.Services.AddTransient(_ =>
{
    return config.GetSection("ApplicationSettings")
                 .Get<ApplicationSettings>();
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRace2GraphqlClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5004/graphql/"));

//builder.Services.AddOidcAuthentication(options =>
//{
//    // Configure your authentication provider options here.
//    // For more information, see https://aka.ms/blazor-standalone-auth
//    builder.Configuration.Bind("oidc", options.ProviderOptions);
//    options.UserOptions.RoleClaim = "role";
//});

await builder.Build().RunAsync();
