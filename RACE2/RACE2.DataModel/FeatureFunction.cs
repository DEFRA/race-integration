using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("FeatureFunctions")]
    public class FeatureFunction
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        [Required]
        public string? name { get; set; }
        [StringLength(64)]
        public string? display_name { get; set; }
        [StringLength(64)]
        public string? description { get; set; }

        [StringLength(64)]
        public string? default_value { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public List<UserPermission> Permission { get; set;} = new List<UserPermission>();
    }
}
