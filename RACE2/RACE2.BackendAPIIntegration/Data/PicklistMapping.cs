using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class PicklistMapping
{
    public int Id { get; set; }

    public string BackEndValueId { get; set; } = null!;

    public DateTime? EndDate { get; set; }

    public int? PicklistValueIdId { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual PicklistDefinition? PicklistValueId { get; set; }
}
