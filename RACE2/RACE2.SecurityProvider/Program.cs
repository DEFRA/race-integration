using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RACE2.DataModel;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.Data;
using RACE2.SecurityProvider.UtilityClasses;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;
string blazorClientURL = _configuration["BlazorClientURL"];

// Add services to the container.
var connectionString = _configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Userdetails>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
var serverConfiguration = new ServerConfiguration();
builder.Services.AddIdentityServer()
            .AddInMemoryApiResources(serverConfiguration.ApiResources)
            .AddInMemoryApiScopes(serverConfiguration.ApiScopes)
            .AddInMemoryIdentityResources(serverConfiguration.IdentityResources)
            .AddInMemoryClients(serverConfiguration.Clients(blazorClientURL))
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<Userdetails>();
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
