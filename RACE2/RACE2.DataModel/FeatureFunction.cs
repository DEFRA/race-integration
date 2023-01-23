using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class FeatureFunction
    {
        [Key, Required]
        public int id { get; set; }
        [StringLength(64)]
        [Required]
        public string name { get; set; }
        [StringLength(64)]
        public string? display_name { get; set; }
        [StringLength(64)]
        public string? description { get; set; }

        [StringLength(64)]
        public string? default_value { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
