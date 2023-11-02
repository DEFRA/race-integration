using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("UserAddress")]
    public class UserAddress
     {
        public int Id { get; set; }

        public string? Type { get; set; }
        [Required]

        public UserDetail? User { get; set; }
        [Required]
        public Address? Address { get; set; }

       

         
    }
}
