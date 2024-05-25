using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Pocracinfdb1402Context : DbContext
{
    public Pocracinfdb1402Context()
    {
    }

    public Pocracinfdb1402Context(DbContextOptions<Pocracinfdb1402Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<ActionsChangeHistory> ActionsChangeHistories { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ApiResource> ApiResources { get; set; }

    public virtual DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

    public virtual DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

    public virtual DbSet<ApiResourceScope> ApiResourceScopes { get; set; }

    public virtual DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; }

    public virtual DbSet<ApiScope> ApiScopes { get; set; }

    public virtual DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

    public virtual DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuditTable> AuditTables { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientClaim> ClientClaims { get; set; }

    public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

    public virtual DbSet<ClientGrantType> ClientGrantTypes { get; set; }

    public virtual DbSet<ClientIdPrestriction> ClientIdPrestrictions { get; set; }

    public virtual DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

    public virtual DbSet<ClientProperty> ClientProperties { get; set; }

    public virtual DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

    public virtual DbSet<ClientScope> ClientScopes { get; set; }

    public virtual DbSet<ClientSecret> ClientSecrets { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentsChangeHistory> CommentsChangeHistories { get; set; }

    public virtual DbSet<ComplianceSummary> ComplianceSummaries { get; set; }

    public virtual DbSet<DeviceCode> DeviceCodes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentEngineer> DocumentEngineers { get; set; }

    public virtual DbSet<DocumentRelationship> DocumentRelationships { get; set; }

    public virtual DbSet<DocumentReservoir> DocumentReservoirs { get; set; }

    public virtual DbSet<DocumentSubmission> DocumentSubmissions { get; set; }

    public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }

    public virtual DbSet<EarlyInspection> EarlyInspections { get; set; }

    public virtual DbSet<FeatureFunction> FeatureFunctions { get; set; }

    public virtual DbSet<FileDetail> FileDetails { get; set; }

    public virtual DbSet<FloodPlan> FloodPlans { get; set; }

    public virtual DbSet<FloodPlanTesting> FloodPlanTestings { get; set; }

    public virtual DbSet<GlobalState> GlobalStates { get; set; }

    public virtual DbSet<IdentityResource> IdentityResources { get; set; }

    public virtual DbSet<IdentityResourceClaim> IdentityResourceClaims { get; set; }

    public virtual DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

    public virtual DbSet<Leases12b616a3a966d30f1394104007> Leases12b616a3a966d30f1394104007s { get; set; }

    public virtual DbSet<Leases22a85ff2a2b4eef91426104121> Leases22a85ff2a2b4eef91426104121s { get; set; }

    public virtual DbSet<Leases6e4a4747d4fc95871458104235> Leases6e4a4747d4fc95871458104235s { get; set; }

    public virtual DbSet<Leases7301bbbd1a8431f21522104463> Leases7301bbbd1a8431f21522104463s { get; set; }

    public virtual DbSet<Leases7eb0e26805f396641426104121> Leases7eb0e26805f396641426104121s { get; set; }

    public virtual DbSet<LeasesB4851b9ee8d380c01522104463> LeasesB4851b9ee8d380c01522104463s { get; set; }

    public virtual DbSet<LeasesCd4854d794dfa5571490104349> LeasesCd4854d794dfa5571490104349s { get; set; }

    public virtual DbSet<LeasesDcd388ab581e08941490104349> LeasesDcd388ab581e08941490104349s { get; set; }

    public virtual DbSet<LeasesE2ffe626d7e0ea9f1394104007> LeasesE2ffe626d7e0ea9f1394104007s { get; set; }

    public virtual DbSet<LeasesF1e3ad866a7ef3181458104235> LeasesF1e3ad866a7ef3181458104235s { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Organisation> Organisations { get; set; }

    public virtual DbSet<OrganisationAddress> OrganisationAddresses { get; set; }

    public virtual DbSet<OrganisationReservoir> OrganisationReservoirs { get; set; }

    public virtual DbSet<PanelMembership> PanelMemberships { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }

    public virtual DbSet<PicklistDefinition> PicklistDefinitions { get; set; }

    public virtual DbSet<PicklistMapping> PicklistMappings { get; set; }

    public virtual DbSet<RawActionSummary> RawActionSummaries { get; set; }

    public virtual DbSet<RawMaintenanceMeasure> RawMaintenanceMeasures { get; set; }

    public virtual DbSet<RawMio> RawMios { get; set; }

    public virtual DbSet<RawStatementDetail> RawStatementDetails { get; set; }

    public virtual DbSet<RawWatchItem> RawWatchItems { get; set; }

    public virtual DbSet<Reservoir> Reservoirs { get; set; }

    public virtual DbSet<ReservoirDetailsChangeHistory> ReservoirDetailsChangeHistories { get; set; }

    public virtual DbSet<SafetyMeasure> SafetyMeasures { get; set; }

    public virtual DbSet<SafetyMeasuresChangeHistory> SafetyMeasuresChangeHistories { get; set; }

    public virtual DbSet<ScreenDefinition> ScreenDefinitions { get; set; }

    public virtual DbSet<ScreenSequence> ScreenSequences { get; set; }

    public virtual DbSet<ScreenSequenceAuditHistory> ScreenSequenceAuditHistories { get; set; }

    public virtual DbSet<StatementDetail> StatementDetails { get; set; }

    public virtual DbSet<SubmissionEmailNotification> SubmissionEmailNotifications { get; set; }

    public virtual DbSet<SubmissionStatus> SubmissionStatuses { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserReservoir> UserReservoirs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:pocracinfss1402.database.windows.net,1433;Initial Catalog=pocracinfdb1402;Persist Security Info=False;User ID=race2admin;Password=Race2Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasIndex(e => e.OwnedByUserId, "IX_Actions_OwnedByUserId");

            entity.HasIndex(e => e.OwnerRoleId, "IX_Actions_OwnerRoleId");

            entity.HasIndex(e => e.ReservoirId, "IX_Actions_ReservoirId");

            entity.Property(e => e.BackEndActionId).HasMaxLength(64);
            entity.Property(e => e.Category).HasMaxLength(64);
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.Frequency).HasMaxLength(64);
            entity.Property(e => e.Notes).HasMaxLength(1024);
            entity.Property(e => e.OwnedByName).HasMaxLength(250);
            entity.Property(e => e.Priority).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.Status)
                .HasMaxLength(64)
                .HasDefaultValue("");
            entity.Property(e => e.Summary).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(64);

            entity.HasOne(d => d.OwnedByUser).WithMany(p => p.Actions).HasForeignKey(d => d.OwnedByUserId);

            entity.HasOne(d => d.OwnerRole).WithMany(p => p.Actions).HasForeignKey(d => d.OwnerRoleId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.Actions).HasForeignKey(d => d.ReservoirId);
        });

        modelBuilder.Entity<ActionsChangeHistory>(entity =>
        {
            entity.ToTable("ActionsChangeHistory");

            entity.HasIndex(e => e.ActionId, "IX_ActionsChangeHistory_ActionId");

            entity.HasIndex(e => e.ChangeByUserId, "IX_ActionsChangeHistory_ChangeByUserId");

            entity.HasIndex(e => e.ReservoirId, "IX_ActionsChangeHistory_ReservoirId");

            entity.HasIndex(e => e.SourceSubmissionId, "IX_ActionsChangeHistory_SourceSubmissionId");

            entity.Property(e => e.FieldName).HasMaxLength(64);
            entity.Property(e => e.NewValue).HasMaxLength(1024);
            entity.Property(e => e.OldValue).HasMaxLength(1024);

            entity.HasOne(d => d.Action).WithMany(p => p.ActionsChangeHistories).HasForeignKey(d => d.ActionId);

            entity.HasOne(d => d.ChangeByUser).WithMany(p => p.ActionsChangeHistories).HasForeignKey(d => d.ChangeByUserId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.ActionsChangeHistories)
                .HasForeignKey(d => d.ReservoirId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SourceSubmission).WithMany(p => p.ActionsChangeHistories)
                .HasForeignKey(d => d.SourceSubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLine1).HasDefaultValue("");
            entity.Property(e => e.BackEndAddressKey).HasMaxLength(64);
        });

        modelBuilder.Entity<ApiResource>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ApiResources_Name").IsUnique();

            entity.Property(e => e.AllowedAccessTokenSigningAlgorithms).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ApiResourceClaim>(entity =>
        {
            entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceClaims_ApiResourceId");

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceClaims).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceProperty>(entity =>
        {
            entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceProperties_ApiResourceId");

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceProperties).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceScope>(entity =>
        {
            entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceScopes_ApiResourceId");

            entity.Property(e => e.Scope).HasMaxLength(200);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceScopes).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceSecret>(entity =>
        {
            entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceSecrets_ApiResourceId");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(4000);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceSecrets).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiScope>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ApiScopes_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ApiScopeClaim>(entity =>
        {
            entity.HasIndex(e => e.ScopeId, "IX_ApiScopeClaims_ScopeId");

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.Scope).WithMany(p => p.ApiScopeClaims).HasForeignKey(d => d.ScopeId);
        });

        modelBuilder.Entity<ApiScopeProperty>(entity =>
        {
            entity.HasIndex(e => e.ScopeId, "IX_ApiScopeProperties_ScopeId");

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.Scope).WithMany(p => p.ApiScopeProperties).HasForeignKey(d => d.ScopeId);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.DisplayName).HasMaxLength(64);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.OrganisationId, "IX_AspNetUsers_OrganisationId");

            entity.HasIndex(e => e.CLastModifiedByUserIdId, "IX_AspNetUsers_cLastModifiedByUserIdId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.BackEndPrimaryRef).HasMaxLength(12);
            entity.Property(e => e.BackEndSecondaryRef).HasMaxLength(12);
            entity.Property(e => e.CAlternativeEmail)
                .HasMaxLength(64)
                .HasColumnName("cAlternativeEmail");
            entity.Property(e => e.CAlternativeEmergencyPhone)
                .HasMaxLength(64)
                .HasColumnName("cAlternativeEmergencyPhone");
            entity.Property(e => e.CAlternativeMobile)
                .HasMaxLength(64)
                .HasColumnName("cAlternativeMobile");
            entity.Property(e => e.CAlternativePhone)
                .HasMaxLength(64)
                .HasColumnName("cAlternativePhone");
            entity.Property(e => e.CBackEndUserId)
                .HasMaxLength(16)
                .HasColumnName("cBackEndUserId");
            entity.Property(e => e.CCreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("cCreatedDate");
            entity.Property(e => e.CDefraId)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cDefraId");
            entity.Property(e => e.CDisplayName)
                .HasMaxLength(250)
                .HasColumnName("cDisplayName");
            entity.Property(e => e.CEmergencyPhone)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cEmergencyPhone");
            entity.Property(e => e.CFirstName)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cFirstName");
            entity.Property(e => e.CFullName)
                .HasMaxLength(250)
                .HasColumnName("cFullName");
            entity.Property(e => e.CIsFirstTimeUser).HasColumnName("cIsFirstTimeUser");
            entity.Property(e => e.CJobTitle)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cJobTitle");
            entity.Property(e => e.CLastAccessDate).HasColumnName("cLastAccessDate");
            entity.Property(e => e.CLastModifiedByUserIdId).HasColumnName("cLastModifiedByUserIdId");
            entity.Property(e => e.CLastModifiedDate).HasColumnName("cLastModifiedDate");
            entity.Property(e => e.CLastName)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cLastName");
            entity.Property(e => e.CMobile)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cMobile");
            entity.Property(e => e.CStatus)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cStatus");
            entity.Property(e => e.CType)
                .HasMaxLength(64)
                .HasDefaultValue(" ")
                .HasColumnName("cType");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.CLastModifiedByUserId).WithMany(p => p.InverseCLastModifiedByUserId).HasForeignKey(d => d.CLastModifiedByUserIdId);

            entity.HasOne(d => d.Organisation).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.OrganisationId);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => e.CId);

            entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserRoles_UserId");

            entity.Property(e => e.CId).HasColumnName("cId");
            entity.Property(e => e.CEndDate).HasColumnName("cEndDate");
            entity.Property(e => e.CStartDate).HasColumnName("cStartDate");
            entity.Property(e => e.CStatus).HasColumnName("cStatus");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUserRoles).HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserRoles).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AuditTable>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_Clients_ClientId").IsUnique();

            entity.Property(e => e.AllowedIdentityTokenSigningAlgorithms).HasMaxLength(100);
            entity.Property(e => e.BackChannelLogoutUri).HasMaxLength(2000);
            entity.Property(e => e.ClientClaimsPrefix).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.ClientName).HasMaxLength(200);
            entity.Property(e => e.ClientUri).HasMaxLength(2000);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.FrontChannelLogoutUri).HasMaxLength(2000);
            entity.Property(e => e.LogoUri).HasMaxLength(2000);
            entity.Property(e => e.PairWiseSubjectSalt).HasMaxLength(200);
            entity.Property(e => e.ProtocolType).HasMaxLength(200);
            entity.Property(e => e.UserCodeType).HasMaxLength(100);
        });

        modelBuilder.Entity<ClientClaim>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientClaims_ClientId");

            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(250);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientClaims).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientCorsOrigin>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientCorsOrigins_ClientId");

            entity.Property(e => e.Origin).HasMaxLength(150);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientCorsOrigins).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientGrantType>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientGrantTypes_ClientId");

            entity.Property(e => e.GrantType).HasMaxLength(250);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientGrantTypes).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientIdPrestriction>(entity =>
        {
            entity.ToTable("ClientIdPRestrictions");

            entity.HasIndex(e => e.ClientId, "IX_ClientIdPRestrictions_ClientId");

            entity.Property(e => e.Provider).HasMaxLength(200);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientIdPrestrictions).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientPostLogoutRedirectUri>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientPostLogoutRedirectUris_ClientId");

            entity.Property(e => e.PostLogoutRedirectUri).HasMaxLength(2000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientPostLogoutRedirectUris).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientProperty>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientProperties_ClientId");

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientProperties).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientRedirectUri>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientRedirectUris_ClientId");

            entity.Property(e => e.RedirectUri).HasMaxLength(2000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientRedirectUris).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientScope>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientScopes_ClientId");

            entity.Property(e => e.Scope).HasMaxLength(200);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientScopes).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientSecret>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientSecrets_ClientId");

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(4000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientSecrets).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.ClosedByUserId, "IX_Comments_ClosedByUserId");

            entity.HasIndex(e => e.CreatedByUserId, "IX_Comments_CreatedByUserId");

            entity.HasIndex(e => e.ParentCommentidId, "IX_Comments_ParentCommentidId");

            entity.HasIndex(e => e.SourceSubmissionId, "IX_Comments_SourceSubmissionId");

            entity.Property(e => e.BackEndCommentId).HasMaxLength(64);
            entity.Property(e => e.CommentText).HasMaxLength(1024);
            entity.Property(e => e.RelatesToObject)
                .HasMaxLength(64)
                .HasDefaultValue("");
            entity.Property(e => e.Status).HasMaxLength(64);

            entity.HasOne(d => d.ClosedByUser).WithMany(p => p.CommentClosedByUsers).HasForeignKey(d => d.ClosedByUserId);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.CommentCreatedByUsers).HasForeignKey(d => d.CreatedByUserId);

            entity.HasOne(d => d.ParentCommentid).WithMany(p => p.InverseParentCommentid).HasForeignKey(d => d.ParentCommentidId);

            entity.HasOne(d => d.SourceSubmission).WithMany(p => p.Comments).HasForeignKey(d => d.SourceSubmissionId);
        });

        modelBuilder.Entity<CommentsChangeHistory>(entity =>
        {
            entity.ToTable("CommentsChangeHistory");

            entity.HasIndex(e => e.ChangeByUserId, "IX_CommentsChangeHistory_ChangeByUserId");

            entity.HasIndex(e => e.CommentId, "IX_CommentsChangeHistory_CommentId");

            entity.HasIndex(e => e.ReservoirId, "IX_CommentsChangeHistory_ReservoirId");

            entity.HasIndex(e => e.SourceSubmissionId, "IX_CommentsChangeHistory_SourceSubmissionId");

            entity.Property(e => e.FieldName).HasMaxLength(64);
            entity.Property(e => e.NewValue).HasMaxLength(1024);
            entity.Property(e => e.OldValue).HasMaxLength(1024);

            entity.HasOne(d => d.ChangeByUser).WithMany(p => p.CommentsChangeHistories).HasForeignKey(d => d.ChangeByUserId);

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentsChangeHistories)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.CommentsChangeHistories).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.SourceSubmission).WithMany(p => p.CommentsChangeHistories)
                .HasForeignKey(d => d.SourceSubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ComplianceSummary>(entity =>
        {
            entity.ToTable("ComplianceSummary");

            entity.HasIndex(e => e.CreatedByUserId, "IX_ComplianceSummary_CreatedByUserId");

            entity.HasIndex(e => e.ReservoirId, "IX_ComplianceSummary_ReservoirId");

            entity.HasIndex(e => e.SourceSubmissionIdId, "IX_ComplianceSummary_SourceSubmissionIdId");

            entity.Property(e => e.Comment).HasMaxLength(1024);
            entity.Property(e => e.ComplianceIndicatorOtherDesc).HasMaxLength(1024);
            entity.Property(e => e.ComplianceIndicatorType).HasMaxLength(64);
            entity.Property(e => e.ComplianceStatus).HasMaxLength(64);
            entity.Property(e => e.CreatedByName).HasMaxLength(250);
            entity.Property(e => e.Reference).HasMaxLength(64);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ComplianceSummaries).HasForeignKey(d => d.CreatedByUserId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.ComplianceSummaries).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.SourceSubmissionId).WithMany(p => p.ComplianceSummaries).HasForeignKey(d => d.SourceSubmissionIdId);
        });

        modelBuilder.Entity<DeviceCode>(entity =>
        {
            entity.HasKey(e => e.UserCode);

            entity.HasIndex(e => e.DeviceCode1, "IX_DeviceCodes_DeviceCode").IsUnique();

            entity.HasIndex(e => e.Expiration, "IX_DeviceCodes_Expiration");

            entity.Property(e => e.UserCode).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DeviceCode1)
                .HasMaxLength(200)
                .HasColumnName("DeviceCode");
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Document");

            entity.HasIndex(e => e.ReservoirId, "IX_Document_ReservoirId");

            entity.HasIndex(e => e.SourceServiceId, "IX_Document_SourceServiceId");

            entity.HasIndex(e => e.UploadByUserId, "IX_Document_UploadByUserId");

            entity.Property(e => e.AvscanDate).HasColumnName("AVScanDate");
            entity.Property(e => e.BackEndDocumentId).HasMaxLength(16);
            entity.Property(e => e.BlobStorageFileName).HasMaxLength(64);
            entity.Property(e => e.CleanFileStorageLink).HasMaxLength(1024);
            entity.Property(e => e.DocumentAuthorName).HasMaxLength(64);
            entity.Property(e => e.DocumentDescription).HasMaxLength(1024);
            entity.Property(e => e.DocumentStatus).HasMaxLength(64);
            entity.Property(e => e.DocumentTitle).HasMaxLength(250);
            entity.Property(e => e.DocumentType).HasMaxLength(64);
            entity.Property(e => e.ProtectiveMarking).HasMaxLength(64);
            entity.Property(e => e.TemplateType).HasMaxLength(64);
            entity.Property(e => e.TemplateVersion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UploadFileLocation).HasMaxLength(1024);
            entity.Property(e => e.UploadFileName).HasMaxLength(100);
            entity.Property(e => e.UploadFileType).HasMaxLength(64);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.Documents).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.UploadByUser).WithMany(p => p.Documents).HasForeignKey(d => d.UploadByUserId);
        });

        modelBuilder.Entity<DocumentEngineer>(entity =>
        {
            entity.ToTable("DocumentEngineer");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentEngineer_DocumentId");

            entity.HasIndex(e => e.EngineerUserId, "IX_DocumentEngineer_EngineerUserId");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentEngineers).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.EngineerUser).WithMany(p => p.DocumentEngineers).HasForeignKey(d => d.EngineerUserId);
        });

        modelBuilder.Entity<DocumentRelationship>(entity =>
        {
            entity.ToTable("DocumentRelationship");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentRelationship_DocumentId");

            entity.HasIndex(e => e.RelatedDocumentId, "IX_DocumentRelationship_RelatedDocumentId");

            entity.Property(e => e.RelationshipType).HasMaxLength(64);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentRelationshipDocuments).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.RelatedDocument).WithMany(p => p.DocumentRelationshipRelatedDocuments)
                .HasForeignKey(d => d.RelatedDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentReservoir>(entity =>
        {
            entity.ToTable("DocumentReservoir");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentReservoir_DocumentId");

            entity.HasIndex(e => e.ReservoirId, "IX_DocumentReservoir_ReservoirId");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentReservoirs).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.DocumentReservoirs).HasForeignKey(d => d.ReservoirId);
        });

        modelBuilder.Entity<DocumentSubmission>(entity =>
        {
            entity.ToTable("DocumentSubmission");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentSubmission_DocumentId");

            entity.HasIndex(e => e.SubmissionId, "IX_DocumentSubmission_SubmissionId");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentSubmissions).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.Submission).WithMany(p => p.DocumentSubmissions).HasForeignKey(d => d.SubmissionId);
        });

        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity.ToTable("DocumentTemplate");

            entity.HasIndex(e => e.OrganisationId, "IX_DocumentTemplate_OrganisationId");

            entity.HasIndex(e => e.ReservoirId, "IX_DocumentTemplate_ReservoirId");

            entity.HasIndex(e => e.ServiceId, "IX_DocumentTemplate_ServiceId");

            entity.HasIndex(e => e.UserId, "IX_DocumentTemplate_UserId");

            entity.Property(e => e.TemplateName).HasMaxLength(260);
            entity.Property(e => e.TemplateType).HasMaxLength(64);
            entity.Property(e => e.TemplateVersion).HasMaxLength(64);

            entity.HasOne(d => d.Organisation).WithMany(p => p.DocumentTemplates).HasForeignKey(d => d.OrganisationId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.DocumentTemplates).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.Service).WithMany(p => p.DocumentTemplates).HasForeignKey(d => d.ServiceId);

            entity.HasOne(d => d.User).WithMany(p => p.DocumentTemplates).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<EarlyInspection>(entity =>
        {
            entity.ToTable("EarlyInspection");

            entity.HasIndex(e => e.ReservoirId, "IX_EarlyInspection_ReservoirId");

            entity.Property(e => e.BackEndEarlyInspectionId).HasMaxLength(64);
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.ReasonSummary).HasMaxLength(200);
            entity.Property(e => e.ReasonType).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.EarlyInspections).HasForeignKey(d => d.ReservoirId);
        });

        modelBuilder.Entity<FeatureFunction>(entity =>
        {
            entity.ToTable("FeatureFunction");

            entity.Property(e => e.Description).HasMaxLength(64);
            entity.Property(e => e.DisplayName).HasMaxLength(64);
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<FileDetail>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FileName).HasMaxLength(80);
        });

        modelBuilder.Entity<FloodPlan>(entity =>
        {
            entity.ToTable("FloodPlan");

            entity.HasIndex(e => e.ReservoirId, "IX_FloodPlan_ReservoirId");

            entity.Property(e => e.BackEndCertificateId).HasMaxLength(64);
            entity.Property(e => e.IsTested).HasMaxLength(64);
            entity.Property(e => e.RequiresRevision).HasMaxLength(64);
            entity.Property(e => e.RevisionDetails).HasMaxLength(1024);
            entity.Property(e => e.RevisionType).HasMaxLength(64);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.FloodPlans).HasForeignKey(d => d.ReservoirId);
        });

        modelBuilder.Entity<FloodPlanTesting>(entity =>
        {
            entity.ToTable("FloodPlanTesting");

            entity.Property(e => e.PlanElement).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.TestDescription).HasMaxLength(1024);
            entity.Property(e => e.TestInterval).HasMaxLength(64);
        });

        modelBuilder.Entity<GlobalState>(entity =>
        {
            entity.HasKey(e => new { e.UserFunctionId, e.UserTableId }).HasName("PK__GlobalSt__A1FEF6DDE3C0DDE7");

            entity.ToTable("GlobalState", "az_func");

            entity.Property(e => e.UserFunctionId)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserFunctionID");
            entity.Property(e => e.UserTableId).HasColumnName("UserTableID");
            entity.Property(e => e.LastAccessTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<IdentityResource>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_IdentityResources_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<IdentityResourceClaim>(entity =>
        {
            entity.HasIndex(e => e.IdentityResourceId, "IX_IdentityResourceClaims_IdentityResourceId");

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.IdentityResource).WithMany(p => p.IdentityResourceClaims).HasForeignKey(d => d.IdentityResourceId);
        });

        modelBuilder.Entity<IdentityResourceProperty>(entity =>
        {
            entity.HasIndex(e => e.IdentityResourceId, "IX_IdentityResourceProperties_IdentityResourceId");

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.IdentityResource).WithMany(p => p.IdentityResourceProperties).HasForeignKey(d => d.IdentityResourceId);
        });

        modelBuilder.Entity<Leases12b616a3a966d30f1394104007>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_1__3214EC0770DF8B47");

            entity.ToTable("Leases_12b616a3a966d30f_1394104007", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<Leases22a85ff2a2b4eef91426104121>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_2__3214EC075C0595C0");

            entity.ToTable("Leases_22a85ff2a2b4eef9_1426104121", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<Leases6e4a4747d4fc95871458104235>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_6__3214EC07C88B59A5");

            entity.ToTable("Leases_6e4a4747d4fc9587_1458104235", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<Leases7301bbbd1a8431f21522104463>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_7__3214EC07EAB0D93C");

            entity.ToTable("Leases_7301bbbd1a8431f2_1522104463", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<Leases7eb0e26805f396641426104121>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_7__3214EC07080A8DA7");

            entity.ToTable("Leases_7eb0e26805f39664_1426104121", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<LeasesB4851b9ee8d380c01522104463>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_b__3214EC07F5CD42FA");

            entity.ToTable("Leases_b4851b9ee8d380c0_1522104463", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<LeasesCd4854d794dfa5571490104349>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_c__3214EC073701286C");

            entity.ToTable("Leases_cd4854d794dfa557_1490104349", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<LeasesDcd388ab581e08941490104349>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_d__3214EC07CBD02A21");

            entity.ToTable("Leases_dcd388ab581e0894_1490104349", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<LeasesE2ffe626d7e0ea9f1394104007>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_e__3214EC0714D74AD5");

            entity.ToTable("Leases_e2ffe626d7e0ea9f_1394104007", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<LeasesF1e3ad866a7ef3181458104235>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leases_f__3214EC076518AF0C");

            entity.ToTable("Leases_f1e3ad866a7ef318_1458104235", "az_func");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AzFuncAttemptCount).HasColumnName("_az_func_AttemptCount");
            entity.Property(e => e.AzFuncChangeVersion).HasColumnName("_az_func_ChangeVersion");
            entity.Property(e => e.AzFuncLeaseExpirationTime).HasColumnName("_az_func_LeaseExpirationTime");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Log");

            entity.Property(e => e.Ip)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.Properties).HasColumnType("xml");
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.ToTable("Organisation");

            entity.Property(e => e.CBackEndOganisationId)
                .HasMaxLength(64)
                .HasColumnName("cBackEndOganisationId");
            entity.Property(e => e.CBusinessType)
                .HasMaxLength(64)
                .HasDefaultValue("")
                .HasColumnName("cBusinessType");
            entity.Property(e => e.OrgName)
                .HasMaxLength(64)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<OrganisationAddress>(entity =>
        {
            entity.ToTable("OrganisationAddress");

            entity.HasIndex(e => e.Addressid, "IX_OrganisationAddress_Addressid");

            entity.HasIndex(e => e.OrganisationId, "IX_OrganisationAddress_OrganisationId");

            entity.Property(e => e.Type).HasMaxLength(64);

            entity.HasOne(d => d.Address).WithMany(p => p.OrganisationAddresses).HasForeignKey(d => d.Addressid);

            entity.HasOne(d => d.Organisation).WithMany(p => p.OrganisationAddresses).HasForeignKey(d => d.OrganisationId);
        });

        modelBuilder.Entity<OrganisationReservoir>(entity =>
        {
            entity.ToTable("OrganisationReservoir");

            entity.HasIndex(e => e.OrganisationId, "IX_OrganisationReservoir_OrganisationId");

            entity.HasIndex(e => e.PrimaryContactUserId, "IX_OrganisationReservoir_PrimaryContactUserId");

            entity.HasIndex(e => e.ReservoirId, "IX_OrganisationReservoir_ReservoirId");

            entity.HasIndex(e => e.SecondaryContactUserId, "IX_OrganisationReservoir_SecondaryContactUserId");

            entity.HasOne(d => d.Organisation).WithMany(p => p.OrganisationReservoirs).HasForeignKey(d => d.OrganisationId);

            entity.HasOne(d => d.PrimaryContactUser).WithMany(p => p.OrganisationReservoirPrimaryContactUsers).HasForeignKey(d => d.PrimaryContactUserId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.OrganisationReservoirs).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.SecondaryContactUser).WithMany(p => p.OrganisationReservoirSecondaryContactUsers).HasForeignKey(d => d.SecondaryContactUserId);
        });

        modelBuilder.Entity<PanelMembership>(entity =>
        {
            entity.ToTable("PanelMembership");

            entity.HasIndex(e => e.UserId, "IX_PanelMembership_UserId");

            entity.Property(e => e.MembershipNumber).HasMaxLength(64);
            entity.Property(e => e.PanelName).HasMaxLength(64);

            entity.HasOne(d => d.User).WithMany(p => p.PanelMemberships).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.HasIndex(e => e.FeatureFunctionId, "IX_Permission_FeatureFunctionId");

            entity.HasIndex(e => e.RoleId, "IX_Permission_RoleId");

            entity.Property(e => e.AccessLevel).HasMaxLength(64);

            entity.HasOne(d => d.FeatureFunction).WithMany(p => p.Permissions).HasForeignKey(d => d.FeatureFunctionId);

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<PersistedGrant>(entity =>
        {
            entity.HasKey(e => e.Key);

            entity.HasIndex(e => e.Expiration, "IX_PersistedGrants_Expiration");

            entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type }, "IX_PersistedGrants_SubjectId_ClientId_Type");

            entity.HasIndex(e => new { e.SubjectId, e.SessionId, e.Type }, "IX_PersistedGrants_SubjectId_SessionId_Type");

            entity.Property(e => e.Key).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<PicklistDefinition>(entity =>
        {
            entity.Property(e => e.DisplayLabel).HasMaxLength(64);
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.PicklistName).HasMaxLength(64);
            entity.Property(e => e.PicklistType).HasMaxLength(64);
            entity.Property(e => e.Value).HasMaxLength(64);
        });

        modelBuilder.Entity<PicklistMapping>(entity =>
        {
            entity.HasIndex(e => e.PicklistValueIdId, "IX_PicklistMappings_PicklistValueIdId");

            entity.Property(e => e.BackEndValueId)
                .HasMaxLength(64)
                .HasDefaultValue("");

            entity.HasOne(d => d.PicklistValueId).WithMany(p => p.PicklistMappings).HasForeignKey(d => d.PicklistValueIdId);
        });

        modelBuilder.Entity<RawActionSummary>(entity =>
        {
            entity.ToTable("RAW_ActionSummary");

            entity.Property(e => e.Action).HasMaxLength(1024);
            entity.Property(e => e.DocumentName).HasMaxLength(64);
            entity.Property(e => e.Mandatory).HasMaxLength(64);
            entity.Property(e => e.Priority).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.ReservoirName).HasMaxLength(200);
        });

        modelBuilder.Entity<RawMaintenanceMeasure>(entity =>
        {
            entity.ToTable("RAW_MaintenanceMeasures");

            entity.Property(e => e.Action).HasMaxLength(1024);
            entity.Property(e => e.Comment).HasMaxLength(1024);
            entity.Property(e => e.DocumentName).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.ReservoirName).HasMaxLength(200);
        });

        modelBuilder.Entity<RawMio>(entity =>
        {
            entity.ToTable("RAW_MIOS");

            entity.Property(e => e.Action).HasMaxLength(1024);
            entity.Property(e => e.Comment).HasMaxLength(1024);
            entity.Property(e => e.DocumentName).HasMaxLength(64);
            entity.Property(e => e.Outstanding).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.ReservoirName).HasMaxLength(200);
        });

        modelBuilder.Entity<RawStatementDetail>(entity =>
        {
            entity.ToTable("RAW_StatementDetails");

            entity.Property(e => e.DocumentName).HasMaxLength(64);
            entity.Property(e => e.GridReference).HasMaxLength(64);
            entity.Property(e => e.HasNoMaintenanceItems).HasMaxLength(1024);
            entity.Property(e => e.HasNoMiositems)
                .HasMaxLength(1024)
                .HasColumnName("HasNoMIOSItems");
            entity.Property(e => e.HasNoWatchItems).HasMaxLength(1024);
            entity.Property(e => e.IsTypeOfStatement122).HasColumnName("IsTypeOfStatement12_2");
            entity.Property(e => e.IsTypeOfStatement122a).HasColumnName("IsTypeOfStatement12_2A");
            entity.Property(e => e.LastInspectingEngineerName).HasMaxLength(250);
            entity.Property(e => e.LastInspectingEngineerPhone).HasMaxLength(64);
            entity.Property(e => e.NearestTown).HasMaxLength(64);
            entity.Property(e => e.ReservoirName).HasMaxLength(200);
            entity.Property(e => e.SignatureStrip).HasMaxLength(64);
            entity.Property(e => e.SupervisingEngineerAddress).HasMaxLength(512);
            entity.Property(e => e.SupervisingEngineerCompany).HasMaxLength(64);
            entity.Property(e => e.SupervisingEngineerEmail).HasMaxLength(64);
            entity.Property(e => e.SupervisingEngineerLong)
                .HasMaxLength(250)
                .HasColumnName("SupervisingEngineer_Long");
            entity.Property(e => e.SupervisingEngineerPhone).HasMaxLength(64);
            entity.Property(e => e.SupervisingEngineerShort)
                .HasMaxLength(250)
                .HasColumnName("SupervisingEngineer_Short");
            entity.Property(e => e.UndertakeName).HasMaxLength(64);
            entity.Property(e => e.UndertakerAddress).HasMaxLength(512);
            entity.Property(e => e.UndertakerEmail).HasMaxLength(64);
            entity.Property(e => e.UndertakerPhone).HasMaxLength(64);
        });

        modelBuilder.Entity<RawWatchItem>(entity =>
        {
            entity.ToTable("RAW_WatchItems");

            entity.Property(e => e.Action).HasMaxLength(1024);
            entity.Property(e => e.Comment).HasMaxLength(1024);
            entity.Property(e => e.DocumentName).HasMaxLength(64);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.ReservoirName).HasMaxLength(200);
        });

        modelBuilder.Entity<Reservoir>(entity =>
        {
            entity.HasIndex(e => e.LastInspectionByUserId, "IX_Reservoirs_LastInspectionByUserId");

            entity.Property(e => e.BackEndReservoirId).HasMaxLength(16);
            entity.Property(e => e.GridReference).HasMaxLength(12);
            entity.Property(e => e.KeyFacts).HasMaxLength(512);
            entity.Property(e => e.LastInspectionEngineerName).HasMaxLength(250);
            entity.Property(e => e.LastInspectionEngineerPhone).HasMaxLength(64);
            entity.Property(e => e.NearestTown).HasMaxLength(64);
            entity.Property(e => e.OperatorType).HasMaxLength(64);
            entity.Property(e => e.PhysicalStatus).HasMaxLength(64);
            entity.Property(e => e.PublicCategory).HasMaxLength(64);
            entity.Property(e => e.PublicName).HasMaxLength(200);
            entity.Property(e => e.ReferenceNumber).HasMaxLength(12);
            entity.Property(e => e.RegisteredCategory).HasMaxLength(64);
            entity.Property(e => e.RegisteredName)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.TopWaterLevel).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.LastInspectionByUser).WithMany(p => p.Reservoirs).HasForeignKey(d => d.LastInspectionByUserId);
        });

        modelBuilder.Entity<ReservoirDetailsChangeHistory>(entity =>
        {
            entity.ToTable("ReservoirDetailsChangeHistory");

            entity.HasIndex(e => e.ChangeByUserId, "IX_ReservoirDetailsChangeHistory_ChangeByUserId");

            entity.HasIndex(e => e.ReservoirId, "IX_ReservoirDetailsChangeHistory_ReservoirId");

            entity.HasIndex(e => e.SourceSubmissionId, "IX_ReservoirDetailsChangeHistory_SourceSubmissionId");

            entity.Property(e => e.FieldName).HasMaxLength(64);
            entity.Property(e => e.NewValue).HasMaxLength(512);
            entity.Property(e => e.OldValue).HasMaxLength(512);

            entity.HasOne(d => d.ChangeByUser).WithMany(p => p.ReservoirDetailsChangeHistories).HasForeignKey(d => d.ChangeByUserId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.ReservoirDetailsChangeHistories).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.SourceSubmission).WithMany(p => p.ReservoirDetailsChangeHistories)
                .HasForeignKey(d => d.SourceSubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SafetyMeasure>(entity =>
        {
            entity.HasIndex(e => e.ReservoirId, "IX_SafetyMeasures_ReservoirId");

            entity.Property(e => e.BackEndSafetyMeasureId).HasMaxLength(16);
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.Notes).HasMaxLength(1024);
            entity.Property(e => e.Othertype).HasMaxLength(200);
            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.Status).HasMaxLength(64);
            entity.Property(e => e.Type).HasMaxLength(64);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.SafetyMeasures)
                .HasForeignKey(d => d.ReservoirId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SafetyMeasuresChangeHistory>(entity =>
        {
            entity.ToTable("SafetyMeasuresChangeHistory");

            entity.HasIndex(e => e.ChangeByUserId, "IX_SafetyMeasuresChangeHistory_ChangeByUserId");

            entity.HasIndex(e => e.MeasureId, "IX_SafetyMeasuresChangeHistory_MeasureId");

            entity.HasIndex(e => e.ReservoirId, "IX_SafetyMeasuresChangeHistory_ReservoirId");

            entity.HasIndex(e => e.SourceSubmissionId, "IX_SafetyMeasuresChangeHistory_SourceSubmissionId");

            entity.Property(e => e.FieldName).HasMaxLength(64);
            entity.Property(e => e.NewValue).HasMaxLength(1024);
            entity.Property(e => e.OldValue).HasMaxLength(1024);

            entity.HasOne(d => d.ChangeByUser).WithMany(p => p.SafetyMeasuresChangeHistories).HasForeignKey(d => d.ChangeByUserId);

            entity.HasOne(d => d.Measure).WithMany(p => p.SafetyMeasuresChangeHistories).HasForeignKey(d => d.MeasureId);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.SafetyMeasuresChangeHistories).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.SourceSubmission).WithMany(p => p.SafetyMeasuresChangeHistories)
                .HasForeignKey(d => d.SourceSubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ScreenDefinition>(entity =>
        {
            entity.ToTable("ScreenDefinition");

            entity.HasIndex(e => e.LastModifiedByUserId, "IX_ScreenDefinition_LastModifiedByUserId");

            entity.Property(e => e.Reference).HasMaxLength(64);
            entity.Property(e => e.Title).HasMaxLength(64);

            entity.HasOne(d => d.LastModifiedByUser).WithMany(p => p.ScreenDefinitions).HasForeignKey(d => d.LastModifiedByUserId);
        });

        modelBuilder.Entity<ScreenSequence>(entity =>
        {
            entity.HasIndex(e => e.ScreenId, "IX_ScreenSequences_ScreenId");

            entity.HasIndex(e => e.ServiceId, "IX_ScreenSequences_ServiceId");

            entity.HasOne(d => d.Screen).WithMany(p => p.ScreenSequences).HasForeignKey(d => d.ScreenId);

            entity.HasOne(d => d.Service).WithMany(p => p.ScreenSequences).HasForeignKey(d => d.ServiceId);
        });

        modelBuilder.Entity<ScreenSequenceAuditHistory>(entity =>
        {
            entity.ToTable("ScreenSequenceAuditHistory");

            entity.HasIndex(e => e.LastModifiedByUserId, "IX_ScreenSequenceAuditHistory_LastModifiedByUserId");

            entity.HasIndex(e => e.ServiceId, "IX_ScreenSequenceAuditHistory_ServiceId");

            entity.HasOne(d => d.LastModifiedByUser).WithMany(p => p.ScreenSequenceAuditHistories).HasForeignKey(d => d.LastModifiedByUserId);

            entity.HasOne(d => d.Service).WithMany(p => p.ScreenSequenceAuditHistories).HasForeignKey(d => d.ServiceId);
        });

        modelBuilder.Entity<StatementDetail>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_StatementDetails_DocumentId");

            entity.Property(e => e.StatementType).HasMaxLength(64);

            entity.HasOne(d => d.Document).WithMany(p => p.StatementDetails).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<SubmissionEmailNotification>(entity =>
        {
            entity.ToTable("SubmissionEmailNotification");

            entity.HasIndex(e => e.SubmissionStatusId, "IX_SubmissionEmailNotification_SubmissionStatusId");

            entity.Property(e => e.ContactType).HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(64);

            entity.HasOne(d => d.SubmissionStatus).WithMany(p => p.SubmissionEmailNotifications).HasForeignKey(d => d.SubmissionStatusId);
        });

        modelBuilder.Entity<SubmissionStatus>(entity =>
        {
            entity.ToTable("SubmissionStatus");

            entity.HasIndex(e => e.LastModifiedByUserId, "IX_SubmissionStatus_LastModifiedByUserId");

            entity.HasIndex(e => e.ReservoirId, "IX_SubmissionStatus_ReservoirId");

            entity.HasIndex(e => e.ServiceId, "IX_SubmissionStatus_ServiceId");

            entity.HasIndex(e => e.SubmittedByUserId, "IX_SubmissionStatus_SubmittedByUserId");

            entity.Property(e => e.RevisionSummary).HasMaxLength(1024);
            entity.Property(e => e.Status).HasDefaultValue("");
            entity.Property(e => e.SubmissionReference)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.LastModifiedByUser).WithMany(p => p.SubmissionStatusLastModifiedByUsers)
                .HasForeignKey(d => d.LastModifiedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.SubmissionStatuses).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.Service).WithMany(p => p.SubmissionStatuses).HasForeignKey(d => d.ServiceId);

            entity.HasOne(d => d.SubmittedByUser).WithMany(p => p.SubmissionStatusSubmittedByUsers)
                .HasForeignKey(d => d.SubmittedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.ToTable("UserAddress");

            entity.HasIndex(e => e.Addressid, "IX_UserAddress_Addressid");

            entity.HasIndex(e => e.UserId, "IX_UserAddress_UserId");

            entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses).HasForeignKey(d => d.Addressid);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserReservoir>(entity =>
        {
            entity.ToTable("UserReservoir");

            entity.HasIndex(e => e.ReservoirId, "IX_UserReservoir_ReservoirId");

            entity.HasIndex(e => e.UserId, "IX_UserReservoir_UserId");

            entity.Property(e => e.BackEndAppointmentId).HasMaxLength(16);

            entity.HasOne(d => d.Reservoir).WithMany(p => p.UserReservoirs).HasForeignKey(d => d.ReservoirId);

            entity.HasOne(d => d.User).WithMany(p => p.UserReservoirs).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
