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
        public int Id { get; set; }
        [StringLength(64)]
        public string? RaceReservoirId { get; set; }
        [StringLength(200)]
        public string? PublicName { get; set; }
        [StringLength(200)]
        public string? RegisteredName { get;set; }
        [StringLength(8)]
        public string? ReferenceNumber { get; set; }
        [StringLength(64)]
        public string? PublicCategory { get; set; }
        [StringLength(64)]
        public string? RegisteredCategory { get;set; }
        [StringLength(12)]
        public string? GridReference { get; set; }       
        public int Capacity { get; set; }
        public int SurfaceArea { get; set; }
        public decimal TopWaterLevel { get; set; }
        public bool HasMultipleDams { get; set; }
        [StringLength(512)]
        public string? KeyFacts { get; set; }
        public DateTime ConstructionStartDate { get; set; }
        public DateTime VerifiedDetailsDate { get; set; }

        public DateTime LastInspectionDate { get; set; }

        public DateTime NextInspectionDate { get; set; }

       // public List<UserDetail> users { get; set; } = new List<UserDetail>();

        public Address? Address { get; set; } = new Address();
        public string? NearestTown { get; set; }
       
       public List<UserReservoir> UserReservoirs { get; set; } = new List<UserReservoir>();

        public List<SupportingDocument> SupportingDocuments { get; set; } = new List<SupportingDocument>();

        [StringLength(64)]
        public string? OperatorType { get; set; }

    }

}
