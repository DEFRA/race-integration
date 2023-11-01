using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("ComplianceSummary")]
    public class ComplianceSummary
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? Reference { get; set; }
        [StringLength(64)]
        public string? ComplianceIndicatorType { get; set; }
        [StringLength(1024)]
        public string? ComplianceIndicatorOtherDesc { get; set; }
        [StringLength(64)]
        public string? ComplianceStatus { get; set; }
        [StringLength(1024)]
        public string? Comment { get; set; }    
        public UserDetail? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set;}

        public int SourceSubmissionId { get; set; }

        public Reservoir Reservoir { get; set;} =  new Reservoir();

    }
}
