using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Action
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Type { get; set; }

    public string? Summary { get; set; }

    public string Description { get; set; } = null!;

    public string? Frequency { get; set; }

    public string? Priority { get; set; }

    public bool? IsComplianceAction { get; set; }

    public string? BackEndActionId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? TargetDate { get; set; }

    public string? Notes { get; set; }

    public int ReservoirId { get; set; }

    public bool IsMandatory { get; set; }

    public string Status { get; set; } = null!;

    public string? OwnedByName { get; set; }

    public int? OwnedByUserId { get; set; }

    public int? OwnerRoleId { get; set; }

    public virtual ICollection<ActionsChangeHistory> ActionsChangeHistories { get; set; } = new List<ActionsChangeHistory>();

    public virtual AspNetUser? OwnedByUser { get; set; }

    public virtual AspNetUserRole? OwnerRole { get; set; }

    public virtual Reservoir Reservoir { get; set; } = null!;
}
