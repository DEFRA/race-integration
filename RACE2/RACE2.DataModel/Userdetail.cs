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

            public string? cBackEndUserId { get; set; }
            public string? cDefraId { get; set; }
           

            public string? cType { get; set; }     
        
            public string? cDisplayName { get; set; }


            public string? cFirstName { get; set; }
         
            public string? cLastName { get; set; }

        [StringLength(250)]
        public string? cFullName { get; set; }

           [StringLength(64)]
           public string? cAlternativeEmail { get; set; }

           public string? cMobile { get; set; }

        [StringLength(64)]
        public string? cAlternativeMobile { get; set; }

        public string? cEmergencyPhone { get; set; }           
            

            [StringLength(64)]
            public string? cAlternativePhone { get; set; }

            [StringLength(64)]
            public string? cAlternativeEmergencyPhone { get; set; }

        // public string? c_organisation_id { get; set; }

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
