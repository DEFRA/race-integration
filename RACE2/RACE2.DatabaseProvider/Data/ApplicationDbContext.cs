using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using System.Data;
using System.Drawing;
using System.IO;

namespace RACE2.DatabaseProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDetail, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DataModel.Action> Actions { get; set; }
        public DbSet<EarlyInspection> EarlyInspections { get; set; }
        public DbSet<FloodPlan> FloodPlans { get; set; }

        public DbSet<SafetyMeasure> SafetyMeasures { get; set; }

        public DbSet<SupportingDocument> Document { get; set; }

        public DbSet<Comment> Comments { get; set; }



        public DbSet<FeatureFunction> FeatureFunctions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<UserReservoir> UserReservoirs { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<PicklistDefinition> PicklistDefinitions { get; set; }

        public DbSet<PicklistMapping> PicklistMappings { get; set; }

        public DbSet<AuditTable> AuditTables { get; set; }

        public DbSet<OrganisationReservoir> OrganisationReservoirs { get; set; }

        public DbSet<OrganisationAddress> OrganisationAddresses { get; set; }

        public DbSet<SubmissionStatus> SubmissionStatus { get; set; }
        public DbSet<ScreenDefinition> ScreenDefinitions { get; set; }

        public DbSet<ScreenSequence> ScreenSequences { get; set; }

        public DbSet<ScreenSequenceAuditHistory> screenSequenceAuditHistories { get; set; }

        public DbSet<ComplianceSummary> ComplianceSummary { get; set; }

        public DbSet<FloodPlanTesting> FloodPlanTesting { get; set; }

        public DbSet<DocumentReservoir> DocumentReservoir { get; set; }

        public DbSet<DocumentEngineer> DocumentEngineer { get; set; }

        public DbSet<DocumentRelationship> DocumentRelationship { get; set; }

        public DbSet<DocumentSubmission> DocumentSubmission { get; set; }

        public DbSet<StatementDetails> StatementDetails { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDetail>(e =>
            {
                e.HasKey(e => e.Id);
                // e.Property(e => e.Id).HasColumnType("integer");
                e.Property(e => e.Id).ValueGeneratedOnAdd();

            });


            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cDefraId)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_parent_userid);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cType)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_display_name)
            //    .HasDefaultValue(" ")
            //    .HasMaxLength(128);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cFirstName)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cLastName)
                .HasDefaultValue(" ")
                .HasMaxLength(64)
                .IsRequired(true);

            


            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cMobile)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cEmergencyPhone)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_organisation_name)
            //    .HasDefaultValue(" ")
            //    .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cJobTitle)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cCurrentPanel)
                .HasDefaultValue(" ")
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cPaon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cSaon)
                .HasDefaultValue(" ")
                .HasMaxLength(64);

            modelBuilder.Entity<UserDetail>()
               .Property(e => e.cStatus)
               .HasDefaultValue(" ")
               .HasMaxLength(64)
               .IsRequired(true);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cCreatedOnDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.cLastAccessDate);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_password)
            //    .HasDefaultValue(" ")
            //    .HasMaxLength(24);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_password_hint)
            //    .HasDefaultValue(" ")
            //    .HasMaxLength(128);

            //modelBuilder.Entity<UserDetail>()
            //    .Property(e => e.c_password_retry_count)
            //    .HasDefaultValue(0);

            modelBuilder.Entity<Role>()
                .Property(e => e.DisplayName)
                .HasMaxLength(64);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description);

            modelBuilder.Entity<Role>()
                .Property(e => e.ParentId);

            modelBuilder.Entity<UserRole>(e =>
            {
                e.HasKey(e => e.cId);
                e.Property(e => e.cId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserRole>()
                .Property(e => e.cStartDate);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.cEndDate);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.cStatus);



            //modelBuilder.Entity<UserDetail>()
            //    .HasMany(left => left.Reservoirs)
            //    .WithMany(right => right.users)
            //    .UsingEntity(join => join.ToTable("UserReservoirs"));


            //modelBuilder.Entity<Address>()
            //    .HasMany(left => left.UserDetail)
            //    .WithMany(right => right.Addresses)
            //    .UsingEntity(join => join.ToTable("UserAddresses"));

            //modelBuilder.Entity<Address>()
            //    .HasMany(left => left.Organisation)
            //    .WithMany(right => right.Addresses)
            //    .UsingEntity(join => join.ToTable("OrganisationAddresses"));
        }
    }

    //    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //    {
    //        public ApplicationDbContext CreateDbContext(string[] args)
    //        {
    //            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
    //            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //            var connectionString = configuration.GetConnectionString("DefaultConnection");
    //            builder.UseSqlServer(connectionString);
    //            return new ApplicationDbContext(builder.Options);
    //        }
    //    }
}