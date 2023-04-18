using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("PicklistMappings")]
    public class PicklistMapping
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? PicklistValue { get; set; }
        [StringLength(64)]
        public string? BackendValue { get; set; }

    }
}
