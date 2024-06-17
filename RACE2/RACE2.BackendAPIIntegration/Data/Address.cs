using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Address
{
    public int Id { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? Town { get; set; }

    public string? County { get; set; }

    public string? Postcode { get; set; }

    public string? BackEndAddressKey { get; set; }

    public virtual ICollection<OrganisationAddress> OrganisationAddresses { get; set; } = new List<OrganisationAddress>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
