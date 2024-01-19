using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReservoirSubmissionDTO
    {
        public int ReservoirId { get; set; }
        public int SubmissionId { get; set; }

        public int SubmittedByUserId { get; set; }
    }
}
