using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class SubmissionStatusDTO
    {
        public FeatureFunction Service { get; set; } = new FeatureFunction();
        [Required]
       public string? PublicName { get; set; }

        public bool IsCurrent { get; set; }
        public bool IsLegacySubmission { get; set; }

        public DateTime DueDate { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public UserDetail ModifiedBy { get; set; } = new UserDetail();
        [Required]
        public ScreenDefinition LastModifiedScreen { get; set; } = new ScreenDefinition();
        [Required]
        public DateTime SubmittedOn { get; set; }
        [Required]
        public UserDetail SubmittedBy { get; set; } = new UserDetail();
    }
}
