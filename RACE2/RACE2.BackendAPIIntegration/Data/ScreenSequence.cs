using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ScreenSequence
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int ScreenId { get; set; }

    public int SequenceNumber { get; set; }

    public virtual ScreenDefinition Screen { get; set; } = null!;

    public virtual FeatureFunction Service { get; set; } = null!;
}
