using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class UserRole : IdentityUserRole<int>
    {
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
