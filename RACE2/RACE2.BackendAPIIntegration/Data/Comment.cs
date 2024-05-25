using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class Comment
{
    public int Id { get; set; }

    public string RelatesToObject { get; set; } = null!;

    public int RelatesToRecordId { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CommentText { get; set; } = null!;

    public int? ParentCommentidId { get; set; }

    public string Status { get; set; } = null!;

    public int? ClosedByUserId { get; set; }

    public DateTime? ClosedDate { get; set; }

    public string? BackEndCommentId { get; set; }

    public bool? IsQualityCheckRequired { get; set; }

    public int? SourceSubmissionId { get; set; }

    public virtual AspNetUser? ClosedByUser { get; set; }

    public virtual ICollection<CommentsChangeHistory> CommentsChangeHistories { get; set; } = new List<CommentsChangeHistory>();

    public virtual AspNetUser CreatedByUser { get; set; } = null!;

    public virtual ICollection<Comment> InverseParentCommentid { get; set; } = new List<Comment>();

    public virtual Comment? ParentCommentid { get; set; }

    public virtual SubmissionStatus? SourceSubmission { get; set; }
}
