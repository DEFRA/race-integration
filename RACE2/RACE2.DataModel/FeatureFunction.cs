using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("FeatureFunction")]
    public class FeatureFunction
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        [Required]
        public string? Name { get; set; }
        [StringLength(64)]
        public string? DisplayName { get; set; }
        [StringLength(64)]
        public string? Description { get; set; }

       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<UserPermission> Permission { get; set;} = new List<UserPermission>();
    }
}
