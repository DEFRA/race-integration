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
        [StringLength(200)]
        public string ReservoirName { get; set; }
        [StringLength(250)]
        public string SupervisingEngineer_Short { get; set; }
       
        public bool IsTypeOfStatement12_2 { get; set; }
        
        public bool IsTypeOfStatement12_2A { get; set; }
        [StringLength(64)]
        public string NearestTown { get; set; }       
        public DateTime? PeriodStart { get; set; }     
        public DateTime? PeriodEnd { get; set; }       
        public DateTime? StatementDate { get; set; }

        [StringLength(64)]
        public string GridReference { get; set; }
        [StringLength(64)]
        public string UndertakeName { get; set; }
        [StringLength(512)]
        public string UndertakerAddress { get; set; }
        [StringLength(64)]
        public string UndertakerEmail { get; set; }
        [StringLength(64)]
        public string UndertakerPhone { get; set; }

        [StringLength(250)]
        public string SupervisingEngineer_Long { get; set; }
        [StringLength(64)]
        public string SupervisingEngineerCompany { get; set; }
        [StringLength(512)]
        public string SupervisingEngineerAddress { get; set; }
        [StringLength(64)]
        public string SupervisingEngineerEmail { get; set; }

        [StringLength(64)]
        public string SupervisingEngineerPhone { get; set; }
        
        public bool HasAlternativeEngineerYes { get; set; }
        
        public bool HasAlternativeEngineerNo { get; set; }
        [StringLength(250)]
        public string LastInspectingEngineerName { get; set; }
        [StringLength(64)]
        public string LastInspectingEngineerPhone { get; set; }

       
        public DateTime LastInspectionDate { get; set; }
        
        public DateTime LastCertificationDate { get; set; }

        
        public bool IsEarlyInspectionRequiredYes { get; set; }
       
        public bool IsEarlyInspectionRequiredNo { get; set; }
       
        public DateTime NextInspectionDate { get; set; }
        [StringLength(1024)]
        public string HasNoMaintenanceItems { get; set; }

        [StringLength(1024)]
        public string HasNoMIOSItems { get; set; }
        [StringLength(1024)]
        public string HasNoWatchItems { get; set; }

        
        public DateTime LastModifiedDateTime { get; set; }
        [StringLength(64)] 
        public string SignatureStrip { get; set; }

        public DateTime? SignatureDate { get; set; }


    }
}
