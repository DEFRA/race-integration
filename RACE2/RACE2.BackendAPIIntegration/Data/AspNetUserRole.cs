using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class AspNetUserRole
{
    public int CId { get; set; }

    public string? CStatus { get; set; }

    public DateTime CStartDate { get; set; }

    public DateTime CEndDate { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual AspNetRole Role { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
