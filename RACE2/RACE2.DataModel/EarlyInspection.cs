using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("EarlyInspections")]
    public class EarlyInspection
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64),Required]
        public string Reference { get; set; }
        [StringLength(64),Required]
        public string ReasonType { get; set; }
        [StringLength(200)]
        public string ReasonSummary { get; set; }
        [StringLength(1024),Required]
        public string Description { get; set;}

        public Reservoir Reservoir { get; set; }

    }

}
