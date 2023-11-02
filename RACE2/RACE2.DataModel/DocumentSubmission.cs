using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("DocumentSubmission")]
    public class DocumentSubmission
    {
        [Key]
        public int Id { get; set; }
        public SupportingDocument Document { get; set; } = new SupportingDocument();

        public SubmissionStatus Submission { get; set; } = new SubmissionStatus();
    }
}
