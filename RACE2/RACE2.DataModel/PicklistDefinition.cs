using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{

    [Table("PicklistDefinitions")]
    public class PicklistDefinition
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64), Required]
        public string PicklistName { get; set; }
        [StringLength(64), Required]
        public string PicklistType { get; set; }
        [StringLength(64), Required]
        public string Value { get; set; }        
        public int DisplayOrder { get; set; }
        [StringLength(64), Required]
        public string DisplayLabel { get; set; }
        public bool isDefault { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
