using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("RAW_MaintenanceMeasures")]
    public class RAW_MaintenanceMeasures
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string DocumentName { get; set; }
        [StringLength(64)]
        public string ReservoirName { get; set; }
        [StringLength(64)]
        public string Reference { get; set; }
        [StringLength(1024)]
        public string Comment { get; set; }

        [StringLength(64)]
        public string LastModifiedDateTime { get; set; }
        [StringLength(1024)]
        public string Action { get; set; }
    }
}
