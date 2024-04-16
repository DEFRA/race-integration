using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class UserSpecificDto : UserDetail
    {
        //public UserDetail userDetail { get; set; } = new UserDetail();
        public List<Role> roles { get; set; } = new List<Role>();
        //  public List<Reservoir> reservoir { get; set;} = new List<Reservoir>();
        public List<Address> addresses { get; set; } = new List<Address>();

        public string? OrganisationName { get; set; }

        public int IsValiduser { get; set; }
    }
}