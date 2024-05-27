using Microsoft.AspNetCore.Http;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;

namespace RACE2.BackendAPIIntegration.Services
{
    public class FileService : IFileService
    {
        public IReservoirRepository _reservoirRepository;
        public IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
        {
            try
            {
                await _fileRepository.PostFileAsync(fileData, fileType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
