using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{

    [Table("SubmissionEmailNotification")]
    public class SubmissionEmailNotification
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int SubmissionStatusId { get; set; }
        [ForeignKey("SubmissionStatusId")]
        public virtual SubmissionStatus SubmissionStatus     { get; set; }
        [Required, StringLength(64)]
        public string Email { get; set; }
        [Required]
        public bool IsOverridePrimaryContact { get; set; }
        [Required, StringLength(64)]
        public string ContactType { get;set; }
    }
}
