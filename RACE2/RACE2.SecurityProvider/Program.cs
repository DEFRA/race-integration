using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Logging;
using RACE2.Logging.Service;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.UtilityClasses;
using RACE2.SecurityProvider.UtilityClasses.CompanyEmployees.OAuth.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//((IConfigurationBuilder)builder.Configuration).Sources.Clear();
//((IConfigurationBuilder)builder.Configuration)
//    //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();

//if (builder.Environment.EnvironmentName == "Development")
//{
//    builder.WebHost.ConfigureKestrel(serverOptions =>
//    {
//        serverOptions.ListenAnyIP(5010, listenOptions => { });
//    });
//}

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.

var connectionString = builder.Configuration["SqlConnection"];
var blazorClientURL = builder.Configuration["ApplicationSettings:RACE2FrontEndURL"];
var webapiURL = builder.Configuration["ApplicationSettings:RACE2WebApiURL"];

//builder.Host.InjectSerilog();
//builder.Services.AddTransient<ILogService, LogService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserDetail>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<UserDetail>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 1;
});

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
    app.UseHsts();
}
app.UseHttpsRedirection();

HostingExtensions.InitializeDatabase(app, blazorClientURL, webapiURL);
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
