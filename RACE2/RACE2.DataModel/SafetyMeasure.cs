using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("SafetyMeasures")]
    public class SafetyMeasure
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? BackEndSafetyMeasureId { get; set; }
        [StringLength(64)]
        public string? Reference { get; set; }
        [StringLength(64)]
        [Required]
        public string? Type { get; set; }
        [StringLength(200)]
        public string? Othertype { get; set; }
        [StringLength(1024)]
        [Required]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime TargetDate { get; set;}
        [Required]
        [StringLength(64)]
        public string? Status { get; set; }
        [StringLength(1024)]
        public string? Notes { get; set; }

        public int ReservoirId { get; set; }
        [ForeignKey("ReservoirId")]
        public virtual Reservoir Reservoir { get; set; }

    }   


}
