using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ScreenDefinition
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public string? Title { get; set; }

    public bool HasSignificantChange { get; set; }

    public DateTime LastModifiedDateTime { get; set; }

    public int LastModifiedByUserId { get; set; }

    public virtual AspNetUser LastModifiedByUser { get; set; } = null!;

    public virtual ICollection<ScreenSequence> ScreenSequences { get; set; } = new List<ScreenSequence>();
}
