using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class SubmissionEmailNotification
{
    public int Id { get; set; }

    public int SubmissionStatusId { get; set; }

    public string Email { get; set; } = null!;

    public bool IsOverridePrimaryContact { get; set; }

    public string ContactType { get; set; } = null!;

    public virtual SubmissionStatus SubmissionStatus { get; set; } = null!;
}
