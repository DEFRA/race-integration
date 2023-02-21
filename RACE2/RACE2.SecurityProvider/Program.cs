using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.UtilityClasses;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
string RACE2FrontEndURL = _configuration["ApplicationSettings:RACE2FrontEndURL"];

// Add services to the container.
var connectionString = _configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserDetail>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
var serverConfiguration = new ServerConfiguration();
builder.Services.AddIdentityServer()
            .AddInMemoryApiResources(serverConfiguration.ApiResources)
            .AddInMemoryApiScopes(serverConfiguration.ApiScopes)
            .AddInMemoryIdentityResources(serverConfiguration.IdentityResources)
            .AddInMemoryClients(serverConfiguration.Clients(RACE2FrontEndURL))
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<UserDetail>();
builder.Services.AddScoped<IRandomPasswordGeneration, RandomPasswordGeneration>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
