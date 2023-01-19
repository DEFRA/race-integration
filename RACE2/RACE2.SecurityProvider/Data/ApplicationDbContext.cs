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
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.type)
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.display_name)
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.first_name)
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.last_name)
                .HasMaxLength(64)
                .IsRequired(true);

           modelBuilder.Entity<Userdetails>()
                .Property(e => e.mobile)
                .HasMaxLength(64);

            
            modelBuilder.Entity<Userdetails>()
                .Property(e => e.emergency_phone)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.organisation_id)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.organisation_name)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.job_title)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.current_panel)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.paon)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.saon)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
               .Property(e => e.status)
               .HasMaxLength(64)
               .IsRequired(true);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.created_on_date);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.last_access_date)
                .HasMaxLength(64);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password)
                .HasMaxLength(24);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password_hint)
                .HasMaxLength(128);

            modelBuilder.Entity<Userdetails>()
                .Property(e => e.password_retry_count)
                .HasMaxLength(64);



        }

    }
}