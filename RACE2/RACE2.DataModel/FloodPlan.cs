using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("FloodPlan")]
    public class FloodPlan
    {
        [Key,Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? BackEndCertificateId { get; set; }
        [Required]
        public DateTime CertificateDate { get; set; }
        [Required,StringLength(64)]
        public string IsTested { get; set; }
        [Required,StringLength(64)]
        public string RequiresRevision { get; set; }
        [StringLength(64)]
        public string RevisionType { get; set; }
        [StringLength(1024)]
        public string RevisionDetails { get; set; }
        [Required]

        public Reservoir Reservoir { get; set; } = new Reservoir();
    }



}
