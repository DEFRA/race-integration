using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class Roles : IdentityRole<int>
    {
        public string? c_display_name { get; set; }
        public string? c_description { get; set; }         
        public int c_parent_id { get;set; }
        public DateTime c_start_date { get; set; }
        public DateTime c_end_date { get; set; }
    }
}
