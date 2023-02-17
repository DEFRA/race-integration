using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RACE2.FrontEndWeb.StateObjects;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
//IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
//string RACE2WebApiURL = _configuration["ApplicationSettings:RACE2WebApiURL"];
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AppState>();

builder.Services.AddRACE2GraphQLClient()
    //.ConfigureHttpClient(client => client.BaseAddress = new Uri(RACE2WebApiURL));
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5003/graphql/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
