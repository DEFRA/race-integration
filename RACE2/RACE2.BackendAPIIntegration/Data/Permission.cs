using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Permission
{
    public int Id { get; set; }

    public string AccessLevel { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? FeatureFunctionId { get; set; }

    public int? RoleId { get; set; }

    public virtual FeatureFunction? FeatureFunction { get; set; }

    public virtual AspNetRole? Role { get; set; }
}
