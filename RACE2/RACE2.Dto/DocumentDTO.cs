using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class DocumentDTO
    {
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string FileType { get; set; }
        public string DocumentType { get; set; }
        public int SuppliedBy { get; set; }
        public DateTime DateSent { get; set; }
        public int SuppliedViaService { get; set; }

        public int ReservoirId { get; set; }
        public int SubmissionId { get; set; }

        public string DocumentName { get; set; }

        public string BlobStorageFileName { get; set; }
        public DateTime AVScanDate { get; set; }

        public bool IsClean { get; set; }

        [StringLength(1024)]
        public string? CleanFileStorageLink { get; set; }
    }
}
