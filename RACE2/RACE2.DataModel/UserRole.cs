using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class UserRole : IdentityUserRole<int>
    {
        [Key, Required]
        public int cId { get; set; }
        public string? cStatus { get; set; }
        public DateTime cStartDate { get; set; }
        public DateTime cEndDate { get; set; }
        //public virtual UserDetail? User { get; set; }
        //public virtual Role? Role { get; set; }



    }
}
