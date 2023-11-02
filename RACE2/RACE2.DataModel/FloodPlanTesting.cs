using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("FloodPlanTesting")]
    public class FloodPlanTesting
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? Reference { get; set; }
        [StringLength(64)]
        public string? PlanElement { get; set; }
        [StringLength(1024)]
        public string? TestDescription { get; set; }
        [StringLength(64)]
        public string? TestInterval { get; set; }
    }
}
