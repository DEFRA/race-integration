using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class AspNetRole
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public int ParentId { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime StartDate { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
