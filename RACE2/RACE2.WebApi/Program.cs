using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using RACE2.WebApi.MutationResolver;
using RACE2.WebApi.QueryResolver;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(o =>
            {
                o.Authority = "http://localhost:5010";
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

builder.Services.AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
        .AddType<RoleQueryResolver>()
    .AddMutationType(m => m.Name("Mutation"))
        .AddType<RoleMutationResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();
