using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class RawActionSummary
{
    public int Id { get; set; }

    public string? DocumentName { get; set; }

    public string? ReservoirName { get; set; }

    public string? Reference { get; set; }

    public string? Mandatory { get; set; }

    public string? Priority { get; set; }

    public DateTime? LastModifiedDateTime { get; set; }

    public string? Action { get; set; }
}
