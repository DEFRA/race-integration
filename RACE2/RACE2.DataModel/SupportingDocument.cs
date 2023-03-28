using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;

namespace RACE2.DataModel
{
    [Table("SupportingDocuments")]
    public class SupportingDocument
    {
        [Key, Required]
        public int Id { get; set; }
        [Required,StringLength(64)]
        public string FileName { get; set; }
        [StringLength(1024)]
        public string FileLocation { get; set; }
        [StringLength(64)]
        public string FileType { get; set; }
        [Required,StringLength(64)]
        public string DocumentName { get; set; }
        [StringLength(1024)]
        public string DocumentDescription { get; set; }
        [Required,StringLength(64)]
        public string DocumentType { get; set; }
        public DateTime DocumentDate { get; set; }
        [StringLength(64)]
        public string DocumentStatus { get; set; }
        [StringLength(64)]
        public string DocumentAuthorName { get; set; }
        [StringLength(64)]
        public string ProtectiveMarking { get; set;}
        [Required]
        public DateTime DateSent { get; set;}

        public DateTime DateReceived { get; set;}

        public UserDetail SuppliedBy { get; set; } = new UserDetail();

        public List<Reservoir> Reservoir { get; set; } = new List<Reservoir>();
    }





}
