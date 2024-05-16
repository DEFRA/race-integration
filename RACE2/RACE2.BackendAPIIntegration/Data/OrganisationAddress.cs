using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class OrganisationAddress
{
    public int? Addressid { get; set; }

    public int? OrganisationId { get; set; }

    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Organisation? Organisation { get; set; }
}
