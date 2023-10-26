using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("OrganisationAddress")]
    public class OrganisationAddress
    {
        public int Id { get; set; }
        public Organisation Organisation { get; set; } = new Organisation();

        public Address Address { get; set; } = new Address();
        [StringLength(64)]
        public string Type { get; set; }
    }
}
