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
    [Table("user_details")]
    public  class Userdetails: IdentityUser    {   
        
           
            public string defra_id { get; set; }
           
            public string type { get; set; }
            
            public string display_name { get; set; }            
           
            public string first_name { get; set; }
         
            public string last_name { get; set; }
           
          
            
            public string mobile { get; set; }
            
          
         
            public string emergency_phone { get; set; }
            
            public string organisation_id { get; set; }
           
            public string organisation_name { get; set; }
           
            public string job_title { get; set; }
          
            public string current_panel { get; set; }
          
            public string paon { get; set; }
           
            public string saon { get; set; }
            
            public string status { get; set; }
         
            public DateTime created_on_date { get; set; }

            public DateTime last_access_date { get; set; }       
        
            public string password { get; set; }        
        
            public string password_hint { get; set; }

            public int password_retry_count { get; set; }
        
    }
}
