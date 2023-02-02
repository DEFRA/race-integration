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
        public string? display_name { get; set; }
        public string? description { get; set; }         
        public int parent_id { get;set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
