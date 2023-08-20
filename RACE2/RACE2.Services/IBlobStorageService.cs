using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream);
        Task<bool> DeleteFileToBlobAsync(string strFileName);
        Task<string> GetBlobSAsTokenByFile(string fileName);
    }
}
