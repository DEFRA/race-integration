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
            Tags.Add("{Supervising Engineer Name}");
            Tags.Add("{Supervising engineer Name}");
            Tags.Add("{Reservoir Nearest town}");
            Tags.Add("{Reservoir Grid reference}");
        }
    }
}
