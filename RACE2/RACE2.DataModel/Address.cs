using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Addresses")]
    public class Address
    {
        [Key, Required]
        public int id { get; set; }
        public string?  AddressType { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string? Country { get; set; }

        public string? Postcode { get; set; }

        public string? NearestPostcode { get; set; }

        public List<UserDetail> UserDetail { get; set; }    = new List<UserDetail>();

        public List<Organisation> Organisation { get; set; } = new List<Organisation>();

    }
}
