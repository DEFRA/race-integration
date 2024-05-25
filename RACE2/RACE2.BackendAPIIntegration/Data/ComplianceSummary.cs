using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ComplianceSummary
{
    public int Id { get; set; }

    public string? Reference { get; set; }

    public string? ComplianceIndicatorType { get; set; }

    public string? ComplianceIndicatorOtherDesc { get; set; }

    public string? ComplianceStatus { get; set; }

    public string? Comment { get; set; }

    public int? SourceSubmissionIdId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public int? ReservoirId { get; set; }

    public string? CreatedByName { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual AspNetUser? CreatedByUser { get; set; }

    public virtual Reservoir? Reservoir { get; set; }

    public virtual SubmissionStatus? SourceSubmissionId { get; set; }
}
