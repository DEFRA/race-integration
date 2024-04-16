using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("SubmissionStatus")]
    public class SubmissionStatus
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(64)]
        public string SubmissionReference { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual FeatureFunction Service { get; set; } = new FeatureFunction();
        public int ReservoirId { get; set; }
        [ForeignKey("ReservoirId")]
        public virtual Reservoir Reservoir { get; set; }

        public bool IsCurrent { get; set; }
        public bool IsLegacySubmission { get; set; }


        public DateTime DueDate { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public DateTime LastModifiedDateTime { get; set; }
        [Required]
        public int LastModifiedByUserId { get; set; }
        [ForeignKey("LastModifiedByUserId")]
        public virtual UserDetail LastModifiedUserDetail { get; set; }
        [Required]
        public int? LastModifiedScreenId { get; set; }
        [Required]
        public DateTime SubmittedDateTime { get; set; }
        [Required]
        public int SubmittedByUserId { get; set; }
        [ForeignKey("SubmittedByUserId")]
        public virtual UserDetail SubmittedUserDetail { get; set; }

        public string? OverrideTemplateName { get; set; }
        public string RevisionSummary { get; set; }

        [Required]
        public bool IsRevision { get; set; }

    }
}
