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
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

//var defaultCredentials = new DefaultAzureCredential();
//builder.Configuration.AddAzureKeyVault(new Uri("https://race2appconfig.azconfig.io"), defaultCredentials,
//                           new AzureKeyVaultConfigurationOptions
//                           {
//                               // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
//                               ReloadInterval = TimeSpan.FromMinutes(5)
//                           });
// Add services to the container.
builder.Services.AddSingleton(builder.Configuration.Get<ApiConfiguration>().AppConfiguration);
var _configuration = builder.Configuration.Get<ApiConfiguration>().AppConfiguration;
builder.Services.AddControllers();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(o =>
            {
                o.Authority = _configuration.RACE2SecurityProviderURL;
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

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();
app.UseVoyager("/graphql","/graphql-voyager");

app.Run();
