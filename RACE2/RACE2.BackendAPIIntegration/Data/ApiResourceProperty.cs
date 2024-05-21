﻿using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ApiResourceProperty
{
    public int Id { get; set; }

    public int ApiResourceId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual ApiResource ApiResource { get; set; } = null!;
}