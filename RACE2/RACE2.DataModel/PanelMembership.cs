using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{    
    [Table("PanelMembership")]
    public class PanelMembership
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public UserDetail? User { get; set; }
        [StringLength(64), Required]
        public string? PanelName { get; set; }
        [StringLength(64)]
        public string? MembershipNumber { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

}
