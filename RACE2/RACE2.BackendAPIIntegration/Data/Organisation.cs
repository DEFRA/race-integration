using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Organisation
{
    public int Id { get; set; }

    public string OrgName { get; set; } = null!;

    public string CBusinessType { get; set; } = null!;

    public string? CBackEndOganisationId { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    public virtual ICollection<OrganisationAddress> OrganisationAddresses { get; set; } = new List<OrganisationAddress>();

    public virtual ICollection<OrganisationReservoir> OrganisationReservoirs { get; set; } = new List<OrganisationReservoir>();
}
