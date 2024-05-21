﻿using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ClientRedirectUri
{
    public int Id { get; set; }

    public string RedirectUri { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}