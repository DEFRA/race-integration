using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class FloodPlan
{
    public int Id { get; set; }

    public DateTime CertificateDate { get; set; }

    public string IsTested { get; set; } = null!;

    public string RequiresRevision { get; set; } = null!;

    public string? RevisionType { get; set; }

    public string? RevisionDetails { get; set; }

    public int ReservoirId { get; set; }

    public string? BackEndCertificateId { get; set; }

    public virtual Reservoir Reservoir { get; set; } = null!;
}
