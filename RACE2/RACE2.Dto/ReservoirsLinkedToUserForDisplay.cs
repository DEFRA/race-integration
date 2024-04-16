using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReservoirsLinkedToUserForDisplay
    {
        public int ReservoirID { get; set; }
        public string ReservoirName { get; set; }
        public string SubmissionReference { get; set; }
        public string UndertakerName { get; set; }
        public string UndertakerEmail { get; set; }
        public string DueDate { get; set; }
        public string SubmittedDateTime { get; set; }
        public string Status { get; set; }

    }
}
