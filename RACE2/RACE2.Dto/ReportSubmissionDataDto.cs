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
        public string ReservoirId { get; set; } = "";
        public string ReservoirRegName { get; set; } = "";
        public string UndertakerName { get; set; } = "";
        public string UndertakerEmail { get; set; } = "";
        public string SubmissionReference { get; set; } = "";
        public string YesNoValueUndertaker { get; set; } = "";
        public string YesNoValueSomeoneAlso { get; set; } = "";
        public string YesNoValueSomeoneElse { get; set; } = "";
        public string SendResendValue { get; set; } = "";
        public string ResubmitReason { get; set; } = "";
        public List<String> EmailListToSomeOne {get;set;} =new List<String>();
        public string EmailEditDelete { get; set; } = "";

    }
}
