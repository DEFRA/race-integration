using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class DocumentEngineer
{
    public int Id { get; set; }

    public int? DocumentId { get; set; }

    public int? EngineerUserId { get; set; }

    public virtual Document? Document { get; set; }

    public virtual AspNetUser? EngineerUser { get; set; }
}
