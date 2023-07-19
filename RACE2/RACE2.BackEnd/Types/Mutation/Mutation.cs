using Azure.Storage.Blobs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace RACE2.BackEnd.Types.Mutation
{
    [MutationType]
    public class Mutation
    {
        private readonly ILogger<Mutation> _logger;
        private readonly IConfiguration _configuration;

        public Mutation(ILogger<Mutation> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<String> UploadFileAsync(int authorId, IFile file, CancellationToken cancellationToken)
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

                BlobClient blobClient = container.GetBlobClient("test.txt");

                stream.Position = 0;
                blobClient.Upload(stream, true);
            }
            return "Success";
        }
    }
}
