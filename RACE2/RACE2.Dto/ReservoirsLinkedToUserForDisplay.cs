using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReservoirsLinkedToUserForDisplay
    {
        public string ReservoirName { get; set; }
        public string UndertakerName { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

    }
}
