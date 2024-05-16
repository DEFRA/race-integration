using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class SafetyMeasure
{
    public int Id { get; set; }

    public string? Reference { get; set; }

    public string Type { get; set; } = null!;

    public string? Othertype { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime TargetDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public int ReservoirId { get; set; }

    public string? BackEndSafetyMeasureId { get; set; }

    public virtual Reservoir Reservoir { get; set; } = null!;

    public virtual ICollection<SafetyMeasuresChangeHistory> SafetyMeasuresChangeHistories { get; set; } = new List<SafetyMeasuresChangeHistory>();
}
