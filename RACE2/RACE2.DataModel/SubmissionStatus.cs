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
        public string SubmissionReference {  get; set; }
        [Required]
        public FeatureFunction Service { get; set; } = new FeatureFunction();
        [Required]
        public Reservoir Reservoir { get; set; } = new Reservoir();

        public bool  IsCurrent { get; set; }
        public bool IsLegacySubmission { get; set; }

        public DateTime DueDate { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]  
        public DateTime LastModifiedDateTime { get; set; }
        [Required]
        public UserDetail LastModifiedBy { get; set; } = new UserDetail();
        [Required]
        public int? LastModifiedScreen { get; set; }
        [Required]
        public DateTime SubmittedDateTime { get; set; }
        [Required]
        public UserDetail SubmittedBy  { get; set;}  = new UserDetail();

        public string? OverrideUsedTemplate { get; set; } 
    }
}
