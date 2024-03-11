using Microsoft.OpenApi.Models;
using RACE2.BackendIntegrationAPI.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    const string securityDefinition = "ApiKey";
    c.AddSecurityDefinition(securityDefinition, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = AuthConstants.ApiKeyHeaderName,
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
