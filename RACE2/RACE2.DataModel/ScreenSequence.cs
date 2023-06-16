using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("ScreenSequences")]
    public class ScreenSequence
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public FeatureFunction Service { get; set; } = new FeatureFunction();
        [Required]
        public ScreenDefinition Screen { get; set; } = new ScreenDefinition();
        [Required]
        public int SequenceNumber { get; set; }
    }
}
