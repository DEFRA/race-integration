using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("StatementDetails")]
    public class StatementDetails
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public int DocumentId {  get; set; }
        [ForeignKey("DocumentId")]
        public virtual SupportingDocument Document { get; set; }
        [Required,StringLength(64)]
        public string StatementType { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }

        public DateTime? StatementDate { get; set; }

        public DateTime? NextStatementDate { get; set; }

        public DateTime? SignatureDate { get; set; }

    }
}
