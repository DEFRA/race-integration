using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class DocumentSubmission
{
    public int Id { get; set; }

    public int? DocumentId { get; set; }

    public int? SubmissionId { get; set; }

    public virtual Document? Document { get; set; }

    public virtual SubmissionStatus? Submission { get; set; }
}
