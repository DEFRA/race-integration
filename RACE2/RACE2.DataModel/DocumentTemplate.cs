using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("DocumentTemplate")]
    public class DocumentTemplate
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, StringLength(64)]
        public string TemplateType { get; set; }
        [Required, StringLength(260)]
        public string TemplateName { get; set; }
        [StringLength(64)]
        public string TemplateVersion { get; set; }
        public DateTime? TemplateStartDate { get; set; }
        public DateTime? TemplateEndDate { get; set; }

        public int? ReservoirId { get; set; }
        [ForeignKey("ReservoirId")]
        public virtual Reservoir Reservoir { get; set; } = new Reservoir();

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDetail UserDetail { get; set; }

        public int? OrganisationId { get; set; }
        [ForeignKey("OrganisationId")]
        public virtual Organisation Organisation { get; set; }

        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual FeatureFunction FeatureFunction { get; set; }

        [Required]
        public bool IsDefaultTemplate { get; set; }






    }
}
