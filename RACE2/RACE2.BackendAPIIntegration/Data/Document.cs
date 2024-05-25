using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Document
{
    public int Id { get; set; }

    public string UploadFileName { get; set; } = null!;

    public string? UploadFileLocation { get; set; }

    public string? TemplateType { get; set; }

    public string? DocumentTitle { get; set; }

    public string? DocumentDescription { get; set; }

    public string DocumentType { get; set; } = null!;

    public DateTime? DocumentDate { get; set; }

    public string? DocumentStatus { get; set; }

    public string? DocumentAuthorName { get; set; }

    public string? ProtectiveMarking { get; set; }

    public DateTime DateSent { get; set; }

    public DateTime? DateReceived { get; set; }

    public int SourceServiceId { get; set; }

    public int? ReservoirId { get; set; }

    public int UploadByUserId { get; set; }

    public string? UploadFileType { get; set; }

    public decimal? TemplateVersion { get; set; }

    public DateTime AvscanDate { get; set; }

    public string? CleanFileStorageLink { get; set; }

    public bool IsClean { get; set; }

    public string? BlobStorageFileName { get; set; }

    public string? BackEndDocumentId { get; set; }

    public virtual ICollection<DocumentEngineer> DocumentEngineers { get; set; } = new List<DocumentEngineer>();

    public virtual ICollection<DocumentRelationship> DocumentRelationshipDocuments { get; set; } = new List<DocumentRelationship>();

    public virtual ICollection<DocumentRelationship> DocumentRelationshipRelatedDocuments { get; set; } = new List<DocumentRelationship>();

    public virtual ICollection<DocumentReservoir> DocumentReservoirs { get; set; } = new List<DocumentReservoir>();

    public virtual ICollection<DocumentSubmission> DocumentSubmissions { get; set; } = new List<DocumentSubmission>();

    public virtual Reservoir? Reservoir { get; set; }

    public virtual ICollection<StatementDetail> StatementDetails { get; set; } = new List<StatementDetail>();

    public virtual AspNetUser UploadByUser { get; set; } = null!;
}
