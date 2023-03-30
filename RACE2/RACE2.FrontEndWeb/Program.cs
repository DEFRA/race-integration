using Fluxor;
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
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IPasswordHasher<UserDetail>, PasswordHasher<UserDetail>>();

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
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
