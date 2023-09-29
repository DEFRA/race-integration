using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class S12ReportTemplateTags
    {
        public List<String> Tags { get; set; } = new List<string>();
        public S12ReportTemplateTags() {
            Tags.Add("{Reservoir name}");
            Tags.Add("{Reservoir Name}");
            Tags.Add("{Reservoir Nearest town}");
            Tags.Add("{Reservoir Grid reference}");
            Tags.Add("{Supervising Engineer Name}");
            Tags.Add("{Supervising engineer Company Name}");
            Tags.Add("{Supervising engineer Address}");
            Tags.Add("{Supervising engineer Email}");
            Tags.Add("{Supervising engineer Phone no.}");
            Tags.Add("{Undertaker Name}");
            Tags.Add("{Undertaker Address}");
            Tags.Add("{Undertaker Email}");
            Tags.Add("{Undertaker Phone no.}");
            Tags.Add("{Other undertakers (optional)}");
        }
    }
}
