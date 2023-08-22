using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class FileUploadViewModel
    {
        public string FileName { get; set; }
        public string FileStorageUrl { get; set; }
        public string ContentType { get; set; }
    }
}
