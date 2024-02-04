using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Comments")]
    public class Comment
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? BackEndCommentId { get; set; }
        [StringLength(64)]
        [Required]
        public string? RelatesToObject { get; set; }
       public int RelatesToRecordId { get; set; }
        [Required]
        public int CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public virtual UserDetail CreatedByUser { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(1024)]
        [Required]
        public string CommentText { get; set; }

        public Comment? ParentCommentid { get; set; }

        [StringLength(64)]
        [Required]
        public string Status { get; set; }

        public UserDetail? ClosedByUser { get; set; }

        public DateTime? ClosedDate { get; set; }

        public bool? IsQualityCheckRequired { get; set; }

        public int? SourceSubmissionId { get; set; }

        [ForeignKey("SourceSubmissionId")]
        public virtual SubmissionStatus SubmissionStatus { get; set; }
    }
}
