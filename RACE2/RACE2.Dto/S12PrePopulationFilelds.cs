using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class S12PrePopulationFields
    {
        public string ReservoirName { get; set; }
        public string ReservoirNearestTown { get; set; }
        public string ReservoirGridRef { get; set; }
        public string SupervisingEngineerName { get; set; }
        public string SupervisingEngineerCompanyName { get; set; }
        public string SupervisingEngineerAddress { get; set; }
        public string SupervisingEngineerEmail { get; set; }
        public string SupervisingEngineerPhoneNumber { get; set; }
        public string UndertakerName { get; set; }
        public string UndertakerAddress { get; set; }
        public string UndertakerEmail { get; set; }
        public string UndertakerPhoneNumber { get; set; }
        public string OtherUndertakers { get; set; }

        public string NextInspectionDate { get; set; }
        public string LastCertificationDate { get; set; }
        public string LastInspectionDate { get; set; }
        public string LastInspectingEngineerName { get; set; }
        public string LastInspectingEngineerPhoneNumber { get; set; }
    }
}
