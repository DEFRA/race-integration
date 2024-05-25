using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class OrganisationReservoir
{
    public int Id { get; set; }

    public int? OrganisationId { get; set; }

    public int? ReservoirId { get; set; }

    public int? PrimaryContactUserId { get; set; }

    public int? SecondaryContactUserId { get; set; }

    public virtual Organisation? Organisation { get; set; }

    public virtual AspNetUser? PrimaryContactUser { get; set; }

    public virtual Reservoir? Reservoir { get; set; }

    public virtual AspNetUser? SecondaryContactUser { get; set; }
}
