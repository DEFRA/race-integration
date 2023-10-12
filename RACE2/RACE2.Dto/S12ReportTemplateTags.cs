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
            Tags.Add("{Reservoir nearest town}");
            Tags.Add("{Reservoir grid reference}");
            Tags.Add("{Supervising engineer name}");
            Tags.Add("{Supervising engineer company name}");
            Tags.Add("{Supervising engineer address}");
            Tags.Add("{Supervising engineer email}");
            Tags.Add("{Supervising engineer phone number}");
            Tags.Add("{Undertaker name}");
            Tags.Add("{Undertaker address}");
            Tags.Add("{Undertaker email}");
            Tags.Add("{Undertaker phone number}");
        }
    }
}
