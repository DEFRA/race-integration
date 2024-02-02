using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Actions")]
    public class Action
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string BackEndActionId { get; set; }
        [StringLength(64),Required]
        public string Reference { get; set; }
        [StringLength(64),Required]
        public string Category { get; set; }
        [StringLength(64)]
        public string Type { get; set; }
        [StringLength(200)]
        public string Summary { get; set; }
        [StringLength(1024),Required]
        public string Description { get; set; }
        [StringLength(64)]
        public string Frequency { get; set; }
        public bool IsMandatory { get; set; }
        [StringLength(64)]
        public string Priority { get; set; }
        public bool? IsComplianceAction { get; set; }

        public int? OwnerRoleId { get; set; }
        [ForeignKey("OwnerRoleId")]
        public virtual UserRole OwnerRole { get; set; }
        public int? OwnedByUserId { get; set; }
        [ForeignKey("OwnedByUserId")]
        public virtual UserDetail OwnedByUser { get; set; }

        [StringLength(250)]
        public string OwnedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public DateTime? TargetDate { get; set; }
        [StringLength(1024)]
        public string Notes { get; set; }

        public int ReservoirId { get; set; }
        [ForeignKey("ReservoirId")]

        public virtual Reservoir Reservoir { get; set; }
        [StringLength(64),Required]
        public string? Status { get; set; }

    }

}
