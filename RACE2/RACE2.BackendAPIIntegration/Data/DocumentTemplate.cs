using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class DocumentTemplate
{
    public int Id { get; set; }

    public string TemplateType { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string? TemplateVersion { get; set; }

    public DateTime? TemplateStartDate { get; set; }

    public DateTime? TemplateEndDate { get; set; }

    public int? ReservoirId { get; set; }

    public int? UserId { get; set; }

    public int? OrganisationId { get; set; }

    public int? ServiceId { get; set; }

    public bool IsDefaultTemplate { get; set; }

    public virtual Organisation? Organisation { get; set; }

    public virtual Reservoir? Reservoir { get; set; }

    public virtual FeatureFunction? Service { get; set; }

    public virtual AspNetUser? User { get; set; }
}
