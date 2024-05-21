using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class ClientProperty
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
