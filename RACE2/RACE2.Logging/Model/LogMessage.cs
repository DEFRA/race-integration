using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Logging.Model
{
    public class LogMessage
    {
        public string UserId { get; set; }

        public DateTime LogTime { get; set; }

        public string Message { get; set; }

        public Exception ExceptionInfo { get; set; }

      //  public ServiceCatagory ServiceCatagory { get; set; }
    }
}
