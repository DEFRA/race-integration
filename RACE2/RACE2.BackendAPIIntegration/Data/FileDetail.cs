using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class FileDetail
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public byte[] FileData { get; set; } = null!;

    public int? FileType { get; set; }
}
