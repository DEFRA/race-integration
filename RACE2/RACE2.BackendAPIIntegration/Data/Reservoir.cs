using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Reservoir
{
    public int Id { get; set; }

    public string? BackEndReservoirId { get; set; }

    public string? PublicName { get; set; }

    public string RegisteredName { get; set; } = null!;

    public string? ReferenceNumber { get; set; }

    public string? PublicCategory { get; set; }

    public string? RegisteredCategory { get; set; }

    public string? GridReference { get; set; }

    public int? Capacity { get; set; }

    public int? SurfaceArea { get; set; }

    public decimal? TopWaterLevel { get; set; }

    public bool? HasMultipleDams { get; set; }

    public string? KeyFacts { get; set; }

    public DateTime? ConstructionStartDate { get; set; }

    public DateTime? VerifiedDetailsDate { get; set; }

    public DateTime? LastInspectionDate { get; set; }

    public DateTime? NextInspectionDate103 { get; set; }

    public string? NearestTown { get; set; }

    public string? OperatorType { get; set; }

    public DateTime? LastCertificationDate { get; set; }

    public string? LastInspectionEngineerName { get; set; }

    public string? LastInspectionEngineerPhone { get; set; }

    public DateTime? NextInspectionDate102 { get; set; }

    public int? LastInspectionByUserId { get; set; }

    public string? PhysicalStatus { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<ActionsChangeHistory> ActionsChangeHistories { get; set; } = new List<ActionsChangeHistory>();

    public virtual ICollection<CommentsChangeHistory> CommentsChangeHistories { get; set; } = new List<CommentsChangeHistory>();

    public virtual ICollection<ComplianceSummary> ComplianceSummaries { get; set; } = new List<ComplianceSummary>();

    public virtual ICollection<DocumentReservoir> DocumentReservoirs { get; set; } = new List<DocumentReservoir>();

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<EarlyInspection> EarlyInspections { get; set; } = new List<EarlyInspection>();

    public virtual ICollection<FloodPlan> FloodPlans { get; set; } = new List<FloodPlan>();

    public virtual AspNetUser? LastInspectionByUser { get; set; }

    public virtual ICollection<OrganisationReservoir> OrganisationReservoirs { get; set; } = new List<OrganisationReservoir>();

    public virtual ICollection<ReservoirDetailsChangeHistory> ReservoirDetailsChangeHistories { get; set; } = new List<ReservoirDetailsChangeHistory>();

    public virtual ICollection<SafetyMeasure> SafetyMeasures { get; set; } = new List<SafetyMeasure>();

    public virtual ICollection<SafetyMeasuresChangeHistory> SafetyMeasuresChangeHistories { get; set; } = new List<SafetyMeasuresChangeHistory>();

    public virtual ICollection<SubmissionStatus> SubmissionStatuses { get; set; } = new List<SubmissionStatus>();

    public virtual ICollection<UserReservoir> UserReservoirs { get; set; } = new List<UserReservoir>();
}
