using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("user_details")]
    public  class Userdetails
    {   
        
            [Key, Required]
            public int id { get; set; }
            [StringLength(64)]
            public string defra_id { get; set; }
            [Required]
            [StringLength(64)]
            public string type { get; set; }
            [StringLength(128)]
            public string display_name { get; set; }
            [Required]
            [StringLength(64)]
            public string first_name { get; set; }
            [Required]
            [StringLength(64)]
            public string last_name { get; set; }
            [Required]
            [StringLength(64)]
            public string email { get; set; }
            [StringLength(64)]
            public string mobile { get; set; }
            [StringLength(64)]
            public string phone { get; set; }
            [StringLength(64)]
            public string emergency_phone { get; set; }
            [StringLength(64)]
            public string organisation_id { get; set; }
            [StringLength(64)]
            public string organisation_name { get; set; }
            [StringLength(64)]
            public string job_title { get; set; }
            [StringLength(64)]
            public string current_panel { get; set; }
            [StringLength(64)]
            public string paon { get; set; }
            [StringLength(64)]
            public string saon { get; set; }
            [StringLength(64)]
            public string primary_role { get; set; }
            [StringLength(64)]
            public string secondary_role { get; set; }
            [Required]
            [StringLength(64)]
            public string status { get; set; }
            [Required]
            [StringLength(64)]
            public DateTime created_on_date { get; set; }
            public DateTime last_access_date { get; set; }
            [StringLength(64)]
            public string password { get; set; }
            [StringLength(128)]
            public string password_hint { get; set; }
            public int password_retry_count { get; set; }
        
    }
}
