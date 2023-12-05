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
    [Table("Document")]
    public class SupportingDocument
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string RaceDocumentId { get; set; }
        [Required,StringLength(100)]
        public string FileName { get; set; }    
        [StringLength(1024)]
        public string FileLocation { get; set; }
        [StringLength(64)]
        public string FileType { get; set; }
        [StringLength(250)]
        public string DocumentName { get; set; }
        [StringLength(1024)]
        public string DocumentDescription { get; set; }
        [Required,StringLength(64)]
        public string DocumentType { get; set; }
        public DateTime? DocumentDate { get; set; }
        [StringLength(64)]
        public string DocumentStatus { get; set; }
        [StringLength(64)]
        public string DocumentAuthorName { get; set; }
        [StringLength(64)]
        public string ProtectiveMarking { get; set;}

        [Required]
        public FeatureFunction SuppliedViaService { get; set; }

        [Required]
        public UserDetail SuppliedBy { get; set; }

        [Required]
        public DateTime DateSent { get; set;}

        public DateTime? DateReceived { get; set;}

        [StringLength(64)]
        public string? UsedTemplateEdition { get; set;}

        public decimal? UsedTemplateVersion { get; set;}

        public DateTime AVScanDate { get; set;}

        public bool IsClean { get; set;}

        [StringLength(1024)]
        public string? CleanFileStorageLink { get; set;}
    }





}
