using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using System.Data;
using System.IO;

namespace RACE2.Dto
{
    public class PersistedGrantDbContext : DbContext
    {

        public PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
        {
            public PersistedGrantDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new PersistedGrantDbContext(builder.Options);
            }
        }
    }
}