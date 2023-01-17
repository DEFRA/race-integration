using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("role_access_rights")]
    public class UserRoleAccess
    {
        [Key, Required]
        public int id { get; set; }
        [StringLength(64)]
        public string primary_role_id { get; set; }
        [StringLength(64)]
        public string secondary_role_id { get; set; }
        [StringLength(64)]
        [Required]
        public string permission_id { get; set; }
        [StringLength(64)]
        [Required]
        public string access_right { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }

    }
}
