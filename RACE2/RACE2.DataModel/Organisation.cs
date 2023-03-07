using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Organisations")]
    public class Organisation
    {
        [Key, Required]
        public int Id { get; set; }
        public string? OrgName { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();

        
    }
}
