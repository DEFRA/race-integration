using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Permission")]
    public class UserPermission
    {
        [Key, Required]
        public int Id { get; set; }     
        [StringLength(64)]
        [Required]
        public string? AccessLevel { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

       // public FeatureFunction FeatureFunction { get; set; } = new FeatureFunction();

      //  public Role Role { get; set; } = new Role();


    }
}
