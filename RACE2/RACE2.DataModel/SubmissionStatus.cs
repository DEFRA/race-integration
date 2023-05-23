using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("SubmissionStatus")]
    public class SubmissionStatus
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
