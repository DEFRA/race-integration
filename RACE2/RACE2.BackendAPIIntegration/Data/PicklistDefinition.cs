using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class PicklistDefinition
{
    public int Id { get; set; }

    public string PicklistName { get; set; } = null!;

    public string PicklistType { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public string DisplayLabel { get; set; } = null!;

    public bool IsDefault { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<PicklistMapping> PicklistMappings { get; set; } = new List<PicklistMapping>();
}
