﻿using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ActionsChangeHistory
{
    public int Id { get; set; }

    public int ReservoirId { get; set; }

    public int SourceSubmissionId { get; set; }

    public int ActionId { get; set; }

    public string FieldName { get; set; } = null!;

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public DateTime ChangeDateTime { get; set; }

    public bool IsBackEndChange { get; set; }

    public int ChangeByUserId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual AspNetUser ChangeByUser { get; set; } = null!;

    public virtual Reservoir Reservoir { get; set; } = null!;

    public virtual SubmissionStatus SourceSubmission { get; set; } = null!;
}