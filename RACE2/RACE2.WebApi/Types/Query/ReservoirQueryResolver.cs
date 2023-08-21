using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;
using System.Security.Cryptography;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.IO.Packaging;
using Azure;
using GreenDonut;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace RACE2.WebApi.Types
{
    [QueryType]
    public class ReservoirQueryResolver
    {
        private readonly ILogger<ReservoirQueryResolver> _logger;
        private readonly IConfiguration _configuration;

        public ReservoirQueryResolver(ILogger<ReservoirQueryResolver> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Reservoir> GetReservoirById(IReservoirService _reservoirService, int id)
        {
            var result = await _reservoirService.GetReservoirById(id);
            return result;
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(IReservoirService _reservoirService, int id)
        {
            var result = await _reservoirService.GetReservoirsByUserId(id);
            return result;
        }

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(IReservoirService _reservoirService, int reservoirid, int category)
        {
            return await _reservoirService.GetActionsListByReservoirIdAndCategory(reservoirid, category);
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(IReservoirService _reservoirService, int reservoirid)
        {
            return await _reservoirService.GetSafetyMeasuresListByReservoirId(reservoirid);
        }

        public async Task<Address> GetAddressByReservoirId(IReservoirService _reservoirService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetAddressByReservoirIdReselvor");
                return await _reservoirService.GetAddressByReservoirId(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(IReservoirService _reservoirService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetOperatorsforReservoir");
                return await _reservoirService.GetOperatorsforReservoir(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(IReservoirService _reservoirService, string emailId)
        {
            var result = await _reservoirService.GetReservoirsByUserEmailId(emailId);
            return result;
        }

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByEmail(IReservoirService _reservoirService, string email)
        {
            try
            {
                _logger.LogInformation("calling GetReservoirStatusByEmail");
                return await _reservoirService.GetReservoirStatusByEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public string WriteContentToBlob(string blobName, string content)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
                {
                    MainDocumentPart mainPart = wordprocessingDocument.AddMainDocumentPart();
                    mainPart.Document =
                         new Document(
                           new Body(
                             new Paragraph(
                               new Run(
                                 new Text(content)))));
                    mainPart.Document.Save();
                }
                // connection to be given in configuration files or env variable  - Get value from step 10 given in article

                var connectionString = _configuration["StorageAccountConnectionString"];
                var containerName = _configuration["StorageAccountContainer"];
                
                // create a client with the connection

                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                container.CreateIfNotExists();
                BlobClient blobClient = container.GetBlobClient(blobName);

                ms.Position = 0;
                blobClient.Upload(ms,true);
            }
            return "Success";
        }

        public string DownloadBlobToLocalFile(string blobName, string fileName)
        {
            var connectionString = _configuration["StorageAccountConnectionString"];
            var containerName = _configuration["StorageAccountContainer"];

            // create a client with the connection

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            BlobClient blobClient = container.GetBlobClient(blobName);
            Stream file = File.OpenWrite(fileName);
            blobClient.DownloadTo(file);
            file.Dispose();
            return "Success";
        }

        public string UploadToBlobFromLocalFile(string blobName,string fileName)
        {
            var connectionString = _configuration["StorageAccountConnectionString"];
            var containerName = _configuration["StorageAccountContainer"];

            // create a client with the connection

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            BlobClient blobClient = container.GetBlobClient(blobName);

            using (Stream stream = File.OpenRead(fileName))
            {
                blobClient.Upload(stream, true);
            }
;
            return "Success";
        }

        //public async Task<string> Upload([FromForm] IFormFile file)
        //{
        //    // Check if the file is there
        //    if (file == null)
        //        return "File is required";

        //    // Get the file name
        //    var fileName = file.FileName;

        //    // Get the extension
        //    var extension = System.IO.Path.GetExtension(fileName);

        //    // Validate the extension based on your business needs

        //    // Generate a new file to avoid duplicates = (FileName withoutExtension - GUId.extension)
        //    var newFileName = $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid().ToString()}{extension}";

        //    // Create the full path of the file including the directory (For this demo we will save the file inside a folder called Data within wwwroot)
        //    var directoryPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data");
        //    var fullPath = System.IO.Path.Combine(directoryPath, newFileName);

        //    // Make sure the directory is there by creating it if it's not exist
        //    Directory.CreateDirectory(directoryPath);

        //    // Create a new file stream where you want to put your file and copy the content from the current file stream to the new one
        //    using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        //    {
        //        // Copy the content to the new stream
        //        await file.CopyToAsync(fileStream);
        //    }

        //    // You are done return the new URL which is (your application url/data/newfilename)
        //    return $"https://localhost:44302/Data/{newFileName}";
        //}

        public void OpenAndAddTextToWordDocument()
        {
            ////Open a WordprocessingDocument for editing using the filepath.
            //WordprocessingDocument wordprocessingDocument =
            //    WordprocessingDocument.Create(@"d:\temp\test.docx", WordprocessingDocumentType.Document);

            //wordprocessingDocument.AddMainDocumentPart();

            //// Create the Document DOM. 
            //wordprocessingDocument.MainDocumentPart.Document =
            //  new Document(
            //    new Body(
            //      new Paragraph(
            //        new Run(
            //          new Text("Hello World!")))));

            //// Save changes to the main document part. 
            //wordprocessingDocument.MainDocumentPart.Document.Save();
            //// Close the handle explicitly.
            //wordprocessingDocument.Dispose();

            //byte[] byteArray = File.ReadAllBytes(@"d:\temp\test.docx");
            //using (MemoryStream mem = new MemoryStream())
            //{
            //    mem.Write(byteArray, 0, (int)byteArray.Length);
            //    using (WordprocessingDocument wordDoc =
            //        WordprocessingDocument.Open(mem, true))
            //    {
            //        // Modify the document as necessary.
            //        // For this example, we insert a new paragraph at the
            //        // beginning of the document.
            //        wordDoc.MainDocumentPart.Document.Body.InsertAt(
            //            new Paragraph(
            //                new Run(
            //                    new Text("Newly inserted paragraph."))), 0);
            //    }
            //    // At this point, the memory stream contains the modified document.
            //    // We could write it back to a SharePoint document library or serve
            //    // it from a web server.

            //    // In this example, we serialize back to the file system to verify
            //    // that the code worked properly.
            //    using (FileStream fileStream = new FileStream(@"d:\temp\test1.docx",
            //        System.IO.FileMode.CreateNew))
            //    {
            //        mem.WriteTo(fileStream);
            //    }
            //}


            using (MemoryStream ms = new MemoryStream())
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
                {
                    MainDocumentPart mainPart = wordprocessingDocument.AddMainDocumentPart();
                    wordprocessingDocument.MainDocumentPart.Document =
                     new Document(
                       new Body(
                         new Paragraph(
                           new Run(
                             new Text("Hello World!")))));
                    mainPart.Document.Save();
                }

                //// In this example, we serialize back to the file system to verify that the code worked properly.
                //using (FileStream fileStream = new FileStream(@"d:\temp\test.docx", System.IO.FileMode.CreateNew))
                //{
                //    ms.WriteTo(fileStream);
                //}

                // connection to be given in configuration files or env variable  - Get value from step 10 given in article

                var connectionString = "DefaultEndpointsProtocol=https;AccountName=race2storageaccount;AccountKey=+voxyaI7i37XXY89mgL3FAg/1JhvSezh1ENdokcV5GMwCOycBYNfYY15aUak3iD+DMvG0Z4kOc6u+ASt0Rq3ZA==;EndpointSuffix=core.windows.net";

                // create a client with the connection

                BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=race2storageaccount;AccountKey=+voxyaI7i37XXY89mgL3FAg/1JhvSezh1ENdokcV5GMwCOycBYNfYY15aUak3iD+DMvG0Z4kOc6u+ASt0Rq3ZA==;EndpointSuffix=core.windows.net","race2webapicontainer");

                // container name which we created                

                BlobClient blobClient = container.GetBlobClient("testdata.docx");

                //var blobHttpHeader = new BlobHttpHeaders();

                //blobHttpHeader.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                //blobClient.Upload(ms, blobHttpHeader); // can use memory stream or file stream or Direct File path
                ms.Position = 0;
                blobClient.Upload(ms);
                
                var u=blobClient.Uri.AbsoluteUri;
                //return blobClient.Uri.AbsoluteUri;
                Stream file = File.OpenWrite(@"c:\temp\testdata11.docx");
                blobClient.DownloadTo(file);
                file.Dispose();
            }
            }
        }
}
