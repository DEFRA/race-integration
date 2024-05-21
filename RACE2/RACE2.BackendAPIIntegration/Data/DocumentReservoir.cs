using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class DocumentReservoir
{
    public int Id { get; set; }

    public int? DocumentId { get; set; }

    public int? ReservoirId { get; set; }

    public virtual Document? Document { get; set; }

    public virtual Reservoir? Reservoir { get; set; }
}
