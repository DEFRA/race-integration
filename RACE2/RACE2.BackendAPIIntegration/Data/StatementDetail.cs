using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class StatementDetail
{
    public int Id { get; set; }

    public int DocumentId { get; set; }

    public string StatementType { get; set; } = null!;

    public DateTime? PeriodStartDate { get; set; }

    public DateTime? PeriodEndDate { get; set; }

    public DateTime? StatementDate { get; set; }

    public DateTime? NextStatementDate { get; set; }

    public DateTime? SignatureDate { get; set; }

    public virtual Document Document { get; set; } = null!;
}
