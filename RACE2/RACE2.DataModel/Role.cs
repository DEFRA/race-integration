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
        public string? DisplayName { get; set; }
        public string? Description { get; set; }         
        public int ParentId { get;set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // public List<UserDetail> UserDetail { get; set; }=new List<UserDetail>();

        public List<UserPermission> Permission { get; set; } =new List<UserPermission>();

    }
}
