using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using RACE2.Dto;

namespace RACE2.BackendAPIIntegration.Services
{
    public interface IFileRepository
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);

        //public Task PostMultiFileAsync(List<DocumentDetails> fileData);

        //public Task DownloadFileById(int fileName);
    }

    public enum FileType
    {
        PDF = 1,
        DOCX = 2
    }
}
