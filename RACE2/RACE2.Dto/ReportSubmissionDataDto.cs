using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReportSubmissionDataDto
    {
        public string UserEmail { get; set; } = "";
        public string ReservoirId { get; set; }  = "";
        public string ReservoirRegName { get; set; } = "";
        public string UndertakerName { get; set; } = "";
        public string UndertakerEmail { get; set; } = "";
        public string SubmissionReference { get; set; } = "";
        public string YesNoValue { get; set; } = "";
        public string SendResendValue { get; set; } = "";
        public string ResubmitReason { get; set; } = "";

    }
}
