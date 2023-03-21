using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class Role : IdentityRole<int>
    {
        public string? c_display_name { get; set; }
        public string? c_description { get; set; }         
        public int c_parent_roleid { get;set; }

       // public List<UserDetail> UserDetail { get; set; }=new List<UserDetail>();

        public List<UserPermission> Permission { get; set; } =new List<UserPermission>();

    }
}
