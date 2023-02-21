using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("C_Permissions")]
    public class UserPermission
    {
        [Key, Required]
        public int id { get; set; }
        public int role_id { get; set; }
        public int feature_function_id { get; set; }
        [StringLength(64)]
        [Required]
        public string? c_access_level { get; set; }
        public DateTime? c_start_date { get; set; }
        public DateTime? c_end_date { get; set; }

        //public List<Role> Roles { get; set; } = new List<Role>();

        //public List<FeatureFunction> FeatureFunctions { get; set; } = new List<FeatureFunction> { };

    }
}
