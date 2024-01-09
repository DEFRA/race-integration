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
    [Table("RAW_StatementDetails")]
    public class RAW_StatementDetails
    {
        [Key, Required]
        public int Id { get; set; }

        [StringLength(64)]
        public string DocumentName { get; set; }
        [StringLength(64)]
        public string ReservoirName { get; set; }
        [StringLength(64)]
        public string SupervisingEngineer_short { get; set; }
        [StringLength(64)]
        public string IsTypeOfStatement12_2 { get; set; }
        [StringLength(64)]
        public string IsTypeOfStatement12_2A { get; set; }
        [StringLength(64)]
        public string NearestTown { get; set; }

        [StringLength(64)]
        public string PeriodStart { get; set; }
        [StringLength(64)]
        public string PeriodEnd { get; set; }
        [StringLength(64)]
        public string StatementDate { get; set; }

        [StringLength(64)]
        public string GridReference { get; set; }
        [StringLength(64)]
        public string UndertakeName { get; set; }
        [StringLength(64)]
        public string UndertakerAddress { get; set; }
        [StringLength(64)]
        public string UndertakerEmail { get; set; }
        [StringLength(64)]
        public string UndertakerPhone { get; set; }

        [StringLength(64)]
        public string SupervisingEngineer_long { get; set; }
        [StringLength(64)]
        public string SupervisingEngineerCompany { get; set; }
        [StringLength(64)]
        public string SupervisingEngineerAddress { get; set; }
        [StringLength(64)]
        public string SupervisingEngineerEmail { get; set; }

        [StringLength(64)]
        public string SupervisingEngineerPhone { get; set; }
        [StringLength(64)]
        public string HasAlternativeEngineerYes { get; set; }
        [StringLength(64)]
        public string HasAlternativeEngineerNo { get; set; }
        [StringLength(64)]
        public string LastInspectingEngineerName { get; set; }
        [StringLength(64)]
        public string LastInspectingEngineerPhone { get; set; }

        [StringLength(64)]
        public string LastInspectionDate { get; set; }
        [StringLength(64)]
        public string LastCertificationDate { get; set; }

        [StringLength(64)]
        public string IsEarlyInspectionRequiredYes { get; set; }
        [StringLength(64)]
        public string IsEarlyInspectionRequiredNo { get; set; }
        [StringLength(64)]
        public string NextInspectionDate { get; set; }
        [StringLength(64)]
        public string HasNoMaintenanceItems { get; set; }

        [StringLength(64)]
        public string HasNoMIOSItems { get; set; }
        [StringLength(64)]
        public string HasNoWatchItems { get; set; }

        [StringLength(64)]
        public string LastModifiedDateTime { get; set; }
        
    }
}
