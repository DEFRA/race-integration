using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class AuditTable
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }
}
