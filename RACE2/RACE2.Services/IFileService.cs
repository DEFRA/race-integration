using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using RACE2.Dto;
using System.Runtime.CompilerServices;

namespace RACE2.BackendAPIIntegration.Services
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);

        //public Task PostMultiFileAsync(List<DocumentDetails> fileData);

        //public Task DownloadFileById(int fileName);
    }

   
}
