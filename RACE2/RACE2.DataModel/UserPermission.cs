using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("UserPermissions")]
    public class UserPermission
    {
        [Key, Required]
        public int id { get; set; }     
        [StringLength(64)]
        [Required]
        public string? access_level { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }


    }
}
