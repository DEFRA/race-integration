using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace RACE2.DataModel
{
    [Table("Logs")]
    public class Log
    {
        [Key,Required]
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }  

        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }

        public XmlDataType Properties { get; set; }
        public string LogEvent { get; set; }
    }
}
