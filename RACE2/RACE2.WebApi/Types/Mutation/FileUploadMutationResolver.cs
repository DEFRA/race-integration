using Path = System.IO.Path;
using System.IO;
using System.Text;
using Azure.Storage.Blobs;

namespace RACE2.WebApi.Types
{ 
   [MutationType]
    public class FileUploadMutationResolver
    {
        private readonly ILogger<FileUploadMutationResolver> _logger;
        private readonly IConfiguration _configuration;

        public FileUploadMutationResolver(ILogger<FileUploadMutationResolver> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ResultData> UploadFileAsync(string blobName, IFile file, CancellationToken cancellationToken)
        {
            //var fileName = file.Name;
            //var fileSize = file.Length;
            //string fileToWriteTo = @"d:\temp\test.png";
            //// We can now work with standard stream functionality of .NET
            //// to handle the file.
            //await using (Stream stream = file.OpenReadStream())
            //{
            //    stream.Position = 0;
            //    using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);
            //    await stream.CopyToAsync(streamToWriteTo, cancellationToken);
            //};
            await using (Stream stream = file.OpenReadStream())
            {
                // connection to be given in configuration files or env variable  - Get value from step 10 given in article

                var connectionString = _configuration["StorageAccountConnectionString"];
                var containerName = _configuration["StorageAccountContainer"];

                // create a client with the connection

                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

                BlobClient blobClient = container.GetBlobClient(blobName);

                stream.Position = 0;
                blobClient.Upload(stream, true);
            }
            return new ResultData("Success");
        }
    }

    public record ResultData(string Result);
}


