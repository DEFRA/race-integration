using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("C_FeatureFunction")]
    public class FeatureFunction
    {
        [Key, Required]
        public int c_Id { get; set; }
        [StringLength(64)]
        [Required]
        public string? c_name { get; set; }
        [StringLength(64)]
        public string? c_display_name { get; set; }
        [StringLength(64)]
        public string? c_description { get; set; }

        [StringLength(64)]
        public string? c_default_value { get; set; }
        public DateTime c_start_date { get; set; }
        public DateTime c_end_date { get; set; }
    }
}
