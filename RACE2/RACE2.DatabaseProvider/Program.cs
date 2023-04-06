using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RACE2.DatabaseProvider;

var builder = WebApplication.CreateBuilder(args);
var migrationAssembly = typeof(Program).Assembly.GetName().Name;
// Add services to the container.
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(ServerConfiguration.IdentityResources)
    .AddTestUsers(ServerConfiguration.GetUsers())
    .AddInMemoryClients(ServerConfiguration.GetClients())
    .AddDeveloperSigningCredential()
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
        .AddOperationalStore(opt =>
        {
            opt.ConfigureDbContext = o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly(migrationAssembly));
        }); ;

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseIdentityServer();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
