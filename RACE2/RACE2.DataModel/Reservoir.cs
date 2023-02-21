using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{

    [Table("Reservoirs")]
    public class Reservoir
    {
        [Key, Required]
        public int id { get; set; }
        [StringLength(64)]
        public string? race_reservoir_id { get; set; }
        [StringLength(200)]
        public string? public_name { get; set; }
        [StringLength(200)]
        public string? registered_name { get;set; }
        [StringLength(8)]
        public string? reference_number { get; set; }
        [StringLength(64)]
        public string? public_category { get; set; }
        [StringLength(64)]
        public string? registered_category { get;set; }
        [StringLength(12)]
        public string? grid_reference { get; set; }
        [StringLength(64)]
        public string? nearest_postcode { get; set; }
        [StringLength(64)]
        public string? nearest_town { get; set; }
        public int capacity { get; set; }
        public int surface_area { get; set; }
        public decimal top_water_level { get; set; }
        public bool has_multiple_dams { get; set; }
        [StringLength(512)]
        public string? key_facts { get; set; }
        public DateTime construction_start_date { get; set; }
        public DateTime verified_details_date { get; set; }

        public DateTime last_inspection_date { get; set; }

        public DateTime next_inspection_date { get; set; }

        public List<Userdetail> users { get; set; } = new List<Userdetail>();

        public Address? address { get; set; }

    }

}
