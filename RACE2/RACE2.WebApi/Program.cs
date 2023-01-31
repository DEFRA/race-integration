using HotChocolate.Execution.Processing;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RACE2.DataAccess.Repository;
using RACE2.SecurityProvider.Data;
using RACE2.WebApi.GraphQL;
using RACE2.WebApi.MutationResolver;
using RACE2.WebApi.QueryResolver;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);
// builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IUserRepository, UserRepository>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:5010";
                o.RequireHttpsMetadata = false;
                o.Audience = "race2WebApiResource";
                o.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        RoleClaimType = "role"
                    };
            });

builder.Services.AddCors(options =>
            { options.AddPolicy("CorsPolicy",
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
        .AddType<UserType>()
            .AddQueryType<UserResolver>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>();

builder.Services.AddDbContext<RACE2.DataAccess.ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL("/graphql");

app.MapControllers();

app.Run();
