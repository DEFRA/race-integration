using Azure.Storage.Blobs.Models;
using RACE2.Dto;
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
        Task<string> UploadFileToBlobAsync(string containerName, string strFileName, string contecntType, Stream fileStream);
        Task<bool> DeleteFileToBlobAsync(string strFileName);
        Task<bool> DeleteFileToBlobAsync(string containerName, string strFileName);
        Task<string> GetBlobAsTokenByFile(string fileName);
        Task<string> GetBlobAsTokenByFile(string containerName, string fileName);
        Task<List<BlobDto>> GetBlobFiles();
        Task<ContentDto> GetBlobFile(string name);
        Task<Stream> GetBlobFileStream(string name);
    }
}
