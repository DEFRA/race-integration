using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class SubmissionStatus
{
    public int Id { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsCurrent { get; set; }

    public bool IsLegacySubmission { get; set; }

    public DateTime LastModifiedDateTime { get; set; }

    public int LastModifiedScreenId { get; set; }

    public string SubmissionReference { get; set; } = null!;

    public int ReservoirId { get; set; }

    public int ServiceId { get; set; }

    public string Status { get; set; } = null!;

    public int SubmittedByUserId { get; set; }

    public DateTime SubmittedDateTime { get; set; }

    public string? OverrideTemplateName { get; set; }

    public int? LastModifiedByUserId { get; set; }

    public string? RevisionSummary { get; set; }

    public bool IsRevision { get; set; }

    public virtual ICollection<ActionsChangeHistory> ActionsChangeHistories { get; set; } = new List<ActionsChangeHistory>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CommentsChangeHistory> CommentsChangeHistories { get; set; } = new List<CommentsChangeHistory>();

    public virtual ICollection<ComplianceSummary> ComplianceSummaries { get; set; } = new List<ComplianceSummary>();

    public virtual ICollection<DocumentSubmission> DocumentSubmissions { get; set; } = new List<DocumentSubmission>();

    public virtual AspNetUser? LastModifiedByUser { get; set; }

    public virtual Reservoir Reservoir { get; set; } = null!;

    public virtual ICollection<ReservoirDetailsChangeHistory> ReservoirDetailsChangeHistories { get; set; } = new List<ReservoirDetailsChangeHistory>();

    public virtual ICollection<SafetyMeasuresChangeHistory> SafetyMeasuresChangeHistories { get; set; } = new List<SafetyMeasuresChangeHistory>();

    public virtual FeatureFunction Service { get; set; } = null!;

    public virtual ICollection<SubmissionEmailNotification> SubmissionEmailNotifications { get; set; } = new List<SubmissionEmailNotification>();

    public virtual AspNetUser SubmittedByUser { get; set; } = null!;
}
