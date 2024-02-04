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
        public string UploadFileName { get; set; }    
        [StringLength(1024)]
        public string UploadFileLocation { get; set; }
        [StringLength(64)]
        public string UploadFileType { get; set; }
        [StringLength(250)]
        public string? DocumentTitle { get; set; }
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
        public int SourceServiceId { get; set; }

        [ForeignKey("SourceServiceId")]
        public virtual FeatureFunction SourceService { get; set; }
        [Required]
        public int UploadByUserId { get; set; }

        [ForeignKey("UploadByUserId")]
        public virtual UserDetail UploadByUser { get; set; }

        [Required]
        public DateTime DateSent { get; set;}

        public DateTime? DateReceived { get; set;}

        [StringLength(64)]
        public string? TemplateType { get; set;}

        public decimal? TemplateVersion { get; set;}

        public DateTime AVScanDate { get; set;}

        public bool IsClean { get; set;}

        [StringLength(1024)]
        public string? CleanFileStorageLink { get; set;}

        [StringLength(64)]
        public string? BlobStorageFileName { get; set;}
    }





}
