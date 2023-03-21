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
            public string? c_defra_id { get; set; }
            public int c_parent_userid { get; set; }

            public string? c_type { get; set; }         
         
            public string? c_first_name { get; set; }
         
            public string? c_last_name { get; set; }        
          
            public string? c_mobile { get; set; }           
                  
            public string? c_emergency_phone { get; set; }
            
           // public string? c_organisation_id { get; set; }
           
                      
            public string? c_job_title { get; set; }
          
            public string? c_current_panel { get; set; }
          
            public string? c_paon { get; set; }
           
            public string? c_saon { get; set; }
            
            public string? c_status { get; set; }
         
            public DateTime c_created_on_date { get; set; }

            public DateTime c_last_access_date { get; set; }

            public bool c_IsFirstTimeUser { get; set; }

          // public List<UserRole> Roles { get; set; } =new List<UserRole>();

            public List<UserReservoir> Reservoirs { get; set; } =new List<UserReservoir>();

            public List<UserAddress> Addresses { get; set; }  = new List<UserAddress>();

            public Organisation? Organisation { get; set; }

         // public Action OwnedBy { get; set; } = new Action();

    }
}
