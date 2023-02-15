using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataAccess
{
    public class AppConfiguration
    {
        public string AllowedHosts { get; set; }
        public string RACE2FrontEndURL { get; set; }
        public string RACE2WebApiURL { get; set; }
        public string RACE2SecurityProviderURL { get; set; }
    }
}
