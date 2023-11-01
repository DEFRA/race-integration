using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("DocumentRelationship")]
    public class DocumentRelationship
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public SupportingDocument Document { get; set; } = new SupportingDocument();

        [Required]
        public SupportingDocument RelatedDocument { get; set; } = new SupportingDocument();

        [StringLength(64),Required]
        public string RelationshipType { get; set; }


    }
}
