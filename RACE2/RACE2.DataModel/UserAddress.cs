using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("UserAddresses")]
    public class UserAddress
     {
        public int Id { get; set; }

        public string? AddressType { get; set; }

        public UserDetail? UserDetail { get; set; } = new UserDetail();

        public Address? Address { get; set; } = new Address();

      
    }
}
