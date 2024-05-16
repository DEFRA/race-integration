using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class EarlyInspection
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public string ReasonType { get; set; } = null!;

    public string? ReasonSummary { get; set; }

    public string Description { get; set; } = null!;

    public int ReservoirId { get; set; }

    public string? BackEndEarlyInspectionId { get; set; }

    public virtual Reservoir Reservoir { get; set; } = null!;
}
