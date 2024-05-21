using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class GlobalState
{
    public string UserFunctionId { get; set; } = null!;

    public int UserTableId { get; set; }

    public long LastSyncVersion { get; set; }

    public DateTime LastAccessTime { get; set; }
}
