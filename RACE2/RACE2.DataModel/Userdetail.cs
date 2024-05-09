using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RACE2.DataModel
{
    [Table("UserDetails")]
    public  class UserDetail: IdentityUser<int>    
    {
        [StringLength(16)]
        public string? cBackEndUserId { get; set; }
        [StringLength(12)]
        public string? BackEndPrimaryRef { get; set; }
        [StringLength(12)]
        public string? BackEndSecondaryRef { get; set; }
        [StringLength(64)]
        public string? cDefraId { get; set; }

        [StringLength(64)]
        public string? cType { get; set; }
        [StringLength(250)]
        public string? cDisplayName { get; set; }

        [StringLength(64)]
        public string? cFirstName { get; set; }
        [StringLength(64)]
        public string? cLastName { get; set; }

        [StringLength(250)]
        public string? cFullName { get; set; }

           [StringLength(64)]
           public string? cAlternativeEmail { get; set; }
        [StringLength(64)]
        public string? cMobile { get; set; }

        [StringLength(64)]
        public string? cAlternativeMobile { get; set; }
        [StringLength(64)]
        public string? cEmergencyPhone { get; set; }           
            

            [StringLength(64)]
            public string? cAlternativePhone { get; set; }

            [StringLength(64)]
            public string? cAlternativeEmergencyPhone { get; set; }

        // public string? c_organisation_id { get; set; }
        [StringLength(64)]
        public string? cJobTitle { get; set; }
          
            //public string? cCurrentPanel { get; set; }
          
            //public string? cPaon { get; set; }
           
            //public string? cSaon { get; set; }
        [Required]
            public string cStatus { get; set; }
         
            public DateTime cCreatedDate { get; set; }

        public DateTime? cLastModifiedDate { get; set; }

        [StringLength(64)]
        public UserDetail? cLastModifiedByUserId { get; set; }

        public DateTime? cLastAccessDate { get; set; }

            public bool? cIsFirstTimeUser { get; set; }

          // public List<UserRole> Roles { get; set; } =new List<UserRole>();

            public List<UserReservoir> Reservoirs { get; set; } =new List<UserReservoir>();

            public List<UserAddress> Addresses { get; set; }  = new List<UserAddress>();

            public Organisation? Organisation { get; set; }

         // public Action OwnedBy { get; set; } = new Action();

    }
}
