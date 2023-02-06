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
        public int c_Id { get; set; }
        public string? c_status { get; set; }
        public DateTime c_start_date { get; set; }
        public DateTime c_end_date { get; set; }
    }
}
