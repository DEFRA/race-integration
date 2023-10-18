using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Address")]
    public class Address
    {
        [Key, Required]
        public int id { get; set; }
        [StringLength(64)]
        public string? RaceAddressKey { get; set; }
        [Required]
        public string? AddressLine1 { get; set; }
        
        public string? AddressLine2 { get; set; }
        public string? Town { get; set; }
        public string? County { get; set; }

        public string? Postcode { get; set; }


       // public List<UserDetail> UserDetail { get; set; }    = new List<UserDetail>();

        //public List<Organisation> Organisation { get; set; } = new List<Organisation>();

    }
}
