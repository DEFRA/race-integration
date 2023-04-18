using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    public class AuditTable
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string? TableName { get; set; }
        [Required]
        public string? ColumnName { get; set; }
        [Required]
        public string? OldValue { get; set; }
        [Required]
        public string? NewValue { get; set; }

        [Required]
        public string? UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }
    }
}
