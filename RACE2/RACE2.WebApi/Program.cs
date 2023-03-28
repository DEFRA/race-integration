using HotChocolate.Execution.Processing;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RACE2.DataAccess.Repository;
using RACE2.Services;
using RACE2.WebApi.Configurations;
using RACE2.WebApi.MutationResolver;
using RACE2.WebApi.QueryResolver;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using HotChocolate.AspNetCore.Voyager;
using RACE2.Logging.Service;
using RACE2.Logging;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//((IConfigurationBuilder)builder.Configuration).Sources.Clear();
//((IConfigurationBuilder)builder.Configuration)
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();

//var defaultCredentials = new DefaultAzureCredential();
//// Create the token credential instance with the client id of the Managed Identity
//var userAssignedIdentityClientId = Environment.GetEnvironmentVariable("AZURE_USER_ASSIGNED_IDENTITY_CLIENT_ID");

//TokenCredential credentials = new ManagedIdentityCredential(userAssignedIdentityClientId);
//#if DEBUG
//credentials = new DefaultAzureCredential(new DefaultAzureCredentialOptions
//{
//    ManagedIdentityClientId = userAssignedIdentityClientId
//});
//#endif
//builder.Configuration.AddAzureKeyVault(new Uri("https://race2keyvault.vault.azure.net/"), credentials,
//                           new AzureKeyVaultConfigurationOptions
//                           {
//                               // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
//                               ReloadInterval = TimeSpan.FromMinutes(5)
//                           });

//var appConfigEndpoint = Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_ENDPOINT");
//var userAssignedIdentityClientId = Environment.GetEnvironmentVariable("AZURE_USER_ASSIGNED_IDENTITY_CLIENT_ID");
//var endpoint = new Uri(appConfigEndpoint);
//// Create the token credential instance with the client id of the Managed Identity
//TokenCredential credentials = new ManagedIdentityCredential(userAssignedIdentityClientId);
//#if DEBUG
//credentials = new DefaultAzureCredential(new DefaultAzureCredentialOptions
//{
//    ManagedIdentityClientId = userAssignedIdentityClientId
//});
//#endif
//builder.Configuration.AddAzureAppConfiguration(options =>
//    options
//        .Connect(endpoint, credentials)
//        .ConfigureKeyVault(kv =>
//        {
//            kv.SetCredential(credentials);
//            kv.SetSecretRefreshInterval(TimeSpan.FromHours(12));
//        })
//        .Select("*", LabelFilter.Null)
//);

//var config = builder.Configuration;
//if (builder.Environment.EnvironmentName == "Development")
//{
//    builder.WebHost.ConfigureKestrel(serverOptions =>
//    {
//        serverOptions.ListenAnyIP(5003, listenOptions => { });
//    });
//}

// Add services to the container.
builder.Services.AddControllers();

//builder.Host.InjectSerilog();
//builder.Services.AddTransient<ILogService, LogService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(o =>
            {
                o.Authority = builder.Configuration["ApplicationSettings:RACE2SecurityProviderURL"];
                o.RequireHttpsMetadata = false;
                o.Audience = "race2WebApi";
                o.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        RoleClaimType = "role"
                    };
            });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


//builder.Services.AddGraphQLServer()
//    .AddQueryType(q => q.Name("Query"))
//        .AddType<RoleQueryResolver>()
//    .AddMutationType(m => m.Name("Mutation"))
//        .AddType<RoleMutationResolver>();

builder.Services.AddGraphQLServer()
    .RegisterService<IUserService>()
    .AddQueryType<UserResolver>()
    .AddMutationType<MutationResolver>()
    .AddAuthorization();

//builder.Services.AddDbContext<RACE2.DataAccess.ApplicationDbContext>(options =>
//options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseSerilogRequestLogging();
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
//app.UseSerilogRequestLogging();

app.MapGraphQL();
app.UseVoyager("/graphql", "/graphql-voyager");

app.Run();