using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class FeatureFunction
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<ScreenSequenceAuditHistory> ScreenSequenceAuditHistories { get; set; } = new List<ScreenSequenceAuditHistory>();

    public virtual ICollection<ScreenSequence> ScreenSequences { get; set; } = new List<ScreenSequence>();

    public virtual ICollection<SubmissionStatus> SubmissionStatuses { get; set; } = new List<SubmissionStatus>();
}
