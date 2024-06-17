﻿using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class IdentityResourceClaim
{
    public int Id { get; set; }

    public int IdentityResourceId { get; set; }

    public string Type { get; set; } = null!;

    public virtual IdentityResource IdentityResource { get; set; } = null!;
}
