using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class RawStatementDetail
{
    public int Id { get; set; }

    public string? DocumentName { get; set; }

    public string? ReservoirName { get; set; }

    public string? SupervisingEngineerShort { get; set; }

    public bool IsTypeOfStatement122 { get; set; }

    public bool IsTypeOfStatement122a { get; set; }

    public string? NearestTown { get; set; }

    public DateTime? PeriodStart { get; set; }

    public DateTime? PeriodEnd { get; set; }

    public DateTime? StatementDate { get; set; }

    public string? GridReference { get; set; }

    public string? UndertakeName { get; set; }

    public string? UndertakerAddress { get; set; }

    public string? UndertakerEmail { get; set; }

    public string? UndertakerPhone { get; set; }

    public string? SupervisingEngineerLong { get; set; }

    public string? SupervisingEngineerCompany { get; set; }

    public string? SupervisingEngineerAddress { get; set; }

    public string? SupervisingEngineerEmail { get; set; }

    public string? SupervisingEngineerPhone { get; set; }

    public bool HasAlternativeEngineerYes { get; set; }

    public bool HasAlternativeEngineerNo { get; set; }

    public string? LastInspectingEngineerName { get; set; }

    public string? LastInspectingEngineerPhone { get; set; }

    public DateTime LastInspectionDate { get; set; }

    public DateTime LastCertificationDate { get; set; }

    public bool IsEarlyInspectionRequiredYes { get; set; }

    public bool IsEarlyInspectionRequiredNo { get; set; }

    public DateTime NextInspectionDate { get; set; }

    public string? HasNoMaintenanceItems { get; set; }

    public string? HasNoMiositems { get; set; }

    public string? HasNoWatchItems { get; set; }

    public DateTime LastModifiedDateTime { get; set; }

    public DateTime? SignatureDate { get; set; }

    public string? SignatureStrip { get; set; }
}
