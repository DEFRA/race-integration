using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using System.Data;
using System.IO;

namespace RACE2.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<Userdetail, Role,int,IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,
        IdentityRoleClaim<int>,IdentityUserToken<int>>
     {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }       

        public DbSet<FeatureFunction> FeatureFunction { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Userdetail>(e =>
            {
                e.HasKey(e => e.Id);
               // e.Property(e => e.Id).HasColumnType("integer");
                e.Property(e => e.Id).ValueGeneratedOnAdd();

            });              
                

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_defra_id)
                .HasDefaultValue(" ")
                .HasMaxLength(64);
                

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_type)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_display_name)
                .HasDefaultValue(" ")
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_first_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_last_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

           modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_mobile)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            
            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_mobile)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_emergency_phone)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_organisation_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_job_title)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_current_panel)
                .HasDefaultValue(" ")
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_paon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_saon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetail>()
               .Property(e => e.c_status)
               .HasDefaultValue(" ")
               .HasMaxLength(64)
               .IsRequired(true);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_created_on_date)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_last_access_date);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_password)
                .HasDefaultValue(" ")
                .HasMaxLength(24);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_password_hint)
                .HasDefaultValue(" ")
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetail>()
                .Property(e => e.c_password_retry_count)
                .HasDefaultValue(0);

            modelBuilder.Entity<Role>()
                .Property(e => e.c_display_name)
                .HasMaxLength(64);

            modelBuilder.Entity<Role>()
                .Property(e => e.c_description);

            modelBuilder.Entity<Role>()
                .Property(e => e.c_parent_id);

            modelBuilder.Entity<Role>()
                .Property(e => e.c_start_date);

            modelBuilder.Entity<Role>()
                .Property(e => e.c_end_date);

            modelBuilder.Entity<UserRole>(e =>
                {
                    e.HasKey( e => e.c_Id);
                    e.Property(e => e.c_Id).ValueGeneratedOnAdd();                   
                });

            modelBuilder.Entity<UserRole>()
                .Property(e => e.c_start_date);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.c_end_date);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.c_status);
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}