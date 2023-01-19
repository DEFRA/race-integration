using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RACE2.DataModel;

namespace RACE2.SecurityProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext<Userdetails>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.defra_id)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.type)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.display_name)
                .HasDefaultValue(" ")
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.first_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.last_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

           modelBuilder.Entity<Userdetails>()
                .Property(e => e.mobile)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            
            modelBuilder.Entity<Userdetails>()
                .Property(e => e.emergency_phone)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.organisation_id)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.organisation_name)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.job_title)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.current_panel)
                .HasDefaultValue(" ")
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.paon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.saon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
               .Property(e => e.status)
               .HasDefaultValue(" ")
               .HasMaxLength(64)
               .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.created_on_date);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.last_access_date);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password)
                .HasDefaultValue(" ")
                .HasMaxLength(24);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password_hint)
                .HasDefaultValue(" ")
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password_retry_count)
                .HasDefaultValue(0);

        }

    }
}