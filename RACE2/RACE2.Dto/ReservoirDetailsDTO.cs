using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReservoirDetailsDTO
    {
        public int Id { get; set; }

        public string? RaceReservoirId { get; set; }  
        public string? PublicName { get; set; }
        public string? RegisteredName { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? PublicCategory { get; set; }
        public string? RegisteredCategory { get; set; }
        public string? GridReference { get; set; }
        public int Capacity { get; set; }
        public int SurfaceArea { get; set; }
        public decimal TopWaterLevel { get; set; }
        public bool HasMultipleDams { get; set; }
        public string? KeyFacts { get; set; }
        public DateTime ConstructionStartDate { get; set; }
        public DateTime VerifiedDetailsDate { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public DateTime NextInspectionDate { get; set; }
        public Address? Address { get; set; } = new Address();
        public string? NearestTown { get; set; }
        public string? OperatorType { get; set; }

    }
}
