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
        public SupportingDocument Document { get; set; } = new SupportingDocument();
        [Required,StringLength(64)]
        public string StatementType { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }

        public DateTime StatementDate { get; set; }

        public DateTime NextStatementDate { get; set; }

    }
}
