using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class DocumentRelationship
{
    public int Id { get; set; }

    public int DocumentId { get; set; }

    public int RelatedDocumentId { get; set; }

    public string RelationshipType { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;

    public virtual Document RelatedDocument { get; set; } = null!;
}
