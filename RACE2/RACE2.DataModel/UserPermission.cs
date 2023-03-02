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
        public int Id { get; set; }     
        [StringLength(64)]
        [Required]
        public string? Access_level { get; set; }
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }


    }
}
