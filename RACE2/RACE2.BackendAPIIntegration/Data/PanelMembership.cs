using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class PanelMembership
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PanelName { get; set; } = null!;

    public string? MembershipNumber { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
