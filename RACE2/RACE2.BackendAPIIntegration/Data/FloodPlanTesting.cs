using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class FloodPlanTesting
{
    public int Id { get; set; }

    public string? Reference { get; set; }

    public string? PlanElement { get; set; }

    public string? TestDescription { get; set; }

    public string? TestInterval { get; set; }
}
