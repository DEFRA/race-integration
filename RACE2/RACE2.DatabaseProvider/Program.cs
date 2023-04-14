using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using RACE2.DatabaseProvider;
using RACE2.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(sqlConnectionString));
builder.Services.AddIdentity<UserDetail, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
var migrationAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString,
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString,
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<UserDetail>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
