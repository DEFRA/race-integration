using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class AspNetUser
{
    public int Id { get; set; }

    public string? CDefraId { get; set; }

    public string CType { get; set; } = null!;

    public string CFirstName { get; set; } = null!;

    public string CLastName { get; set; } = null!;

    public string? CMobile { get; set; }

    public string? CEmergencyPhone { get; set; }

    public string? CJobTitle { get; set; }

    public string CStatus { get; set; } = null!;

    public DateTime CCreatedDate { get; set; }

    public DateTime? CLastAccessDate { get; set; }

    public bool? CIsFirstTimeUser { get; set; }

    public int? OrganisationId { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? CAlternativeEmail { get; set; }

    public string? CAlternativeEmergencyPhone { get; set; }

    public string? CAlternativeMobile { get; set; }

    public string? CAlternativePhone { get; set; }

    public string? CBackEndUserId { get; set; }

    public string? CDisplayName { get; set; }

    public string? CFullName { get; set; }

    public int? CLastModifiedByUserIdId { get; set; }

    public DateTime? CLastModifiedDate { get; set; }

    public string? BackEndPrimaryRef { get; set; }

    public string? BackEndSecondaryRef { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<ActionsChangeHistory> ActionsChangeHistories { get; set; } = new List<ActionsChangeHistory>();

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual AspNetUser? CLastModifiedByUserId { get; set; }

    public virtual ICollection<Comment> CommentClosedByUsers { get; set; } = new List<Comment>();

    public virtual ICollection<Comment> CommentCreatedByUsers { get; set; } = new List<Comment>();

    public virtual ICollection<CommentsChangeHistory> CommentsChangeHistories { get; set; } = new List<CommentsChangeHistory>();

    public virtual ICollection<ComplianceSummary> ComplianceSummaries { get; set; } = new List<ComplianceSummary>();

    public virtual ICollection<DocumentEngineer> DocumentEngineers { get; set; } = new List<DocumentEngineer>();

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<AspNetUser> InverseCLastModifiedByUserId { get; set; } = new List<AspNetUser>();

    public virtual Organisation? Organisation { get; set; }

    public virtual ICollection<OrganisationReservoir> OrganisationReservoirPrimaryContactUsers { get; set; } = new List<OrganisationReservoir>();

    public virtual ICollection<OrganisationReservoir> OrganisationReservoirSecondaryContactUsers { get; set; } = new List<OrganisationReservoir>();

    public virtual ICollection<PanelMembership> PanelMemberships { get; set; } = new List<PanelMembership>();

    public virtual ICollection<ReservoirDetailsChangeHistory> ReservoirDetailsChangeHistories { get; set; } = new List<ReservoirDetailsChangeHistory>();

    public virtual ICollection<Reservoir> Reservoirs { get; set; } = new List<Reservoir>();

    public virtual ICollection<SafetyMeasuresChangeHistory> SafetyMeasuresChangeHistories { get; set; } = new List<SafetyMeasuresChangeHistory>();

    public virtual ICollection<ScreenDefinition> ScreenDefinitions { get; set; } = new List<ScreenDefinition>();

    public virtual ICollection<ScreenSequenceAuditHistory> ScreenSequenceAuditHistories { get; set; } = new List<ScreenSequenceAuditHistory>();

    public virtual ICollection<SubmissionStatus> SubmissionStatusLastModifiedByUsers { get; set; } = new List<SubmissionStatus>();

    public virtual ICollection<SubmissionStatus> SubmissionStatusSubmittedByUsers { get; set; } = new List<SubmissionStatus>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserReservoir> UserReservoirs { get; set; } = new List<UserReservoir>();
}
