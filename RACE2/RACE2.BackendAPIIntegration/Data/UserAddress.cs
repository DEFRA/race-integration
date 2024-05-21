using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class UserAddress
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int UserId { get; set; }

    public int Addressid { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
