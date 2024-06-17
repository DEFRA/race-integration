using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ScreenSequenceAuditHistory
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public string ChangeEvent { get; set; } = null!;

    public int OldValue { get; set; }

    public int NewValue { get; set; }

    public DateTime LastModifiedDateTime { get; set; }

    public int LastModifiedByUserId { get; set; }

    public virtual AspNetUser LastModifiedByUser { get; set; } = null!;

    public virtual FeatureFunction Service { get; set; } = null!;
}
