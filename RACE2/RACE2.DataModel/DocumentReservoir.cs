using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("DocumentReservoir")]
    public class DocumentReservoir
    {

        [Key]
        public int Id { get; set; }
        public SupportingDocument  Document { get; set; } = new SupportingDocument();

        public Reservoir Reservoir { get; set; } =  new Reservoir();    
    }
}
