using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RACE2.Dto;

namespace RACE2.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BlobStorageService> _logger;
        private string blobStorageconnection = string.Empty;
        private string blobContainerName = string.Empty;
        public BlobStorageService(IConfiguration configuration, ILogger<BlobStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            blobStorageconnection = _configuration["StorageAccountConnectionString"];
            blobContainerName = _configuration["StorageAccountContainer"];
        }
        public async Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream)
        {
            try
            {
                var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
                container.CreateIfNotExists();
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contecntType });
                var urlString = blob.Uri.ToString();
                return urlString;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<string> UploadFileToBlobAsync(string containerName, string strFileName, string contecntType, Stream fileStream)
        {
            try
            {
                var container = new BlobContainerClient(blobStorageconnection, containerName);
                container.CreateIfNotExists();
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contecntType });
                var urlString = blob.Uri.ToString();
                return urlString;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteFileToBlobAsync(string strFileName)
        {
            try
            {
                var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<bool> DeleteFileToBlobAsync(string containerName, string strFileName)
        {
            try
            {
                var container = new BlobContainerClient(blobStorageconnection, containerName);
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<string> GetBlobAsTokenByFile(string fileName)
        {
            try
            {
                var azureStorageAccount = _configuration["StorageAccount"]; ;
                var azureStorageAccessKey = _configuration["StorageAccessKey"]; 
                Azure.Storage.Sas.BlobSasBuilder blobSasBuilder = new Azure.Storage.Sas.BlobSasBuilder()
                {
                    BlobContainerName = blobContainerName,
                    BlobName = fileName,
                    ExpiresOn = DateTime.UtcNow.AddMinutes(2),//Let SAS token expire after 5 minutes.
                };
                blobSasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);//User will only be able to read the blob and it's properties
                var sasToken = blobSasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(azureStorageAccount,azureStorageAccessKey)).ToString();
                return sasToken;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> GetBlobAsTokenByFile(string containerName, string fileName)
        {
            try
            {
                var azureStorageAccount = _configuration["StorageAccount"]; ;
                var azureStorageAccessKey = _configuration["StorageAccessKey"];
                Azure.Storage.Sas.BlobSasBuilder blobSasBuilder = new Azure.Storage.Sas.BlobSasBuilder()
                {
                    BlobContainerName = containerName,
                    BlobName = fileName,
                    ExpiresOn = DateTime.UtcNow.AddMinutes(2),//Let SAS token expire after 5 minutes.
                };
                blobSasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);//User will only be able to read the blob and it's properties
                var sasToken = blobSasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(azureStorageAccount, azureStorageAccessKey)).ToString();
                return sasToken;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BlobDto>> GetBlobFiles()
        {
            var blobs = new List<BlobDto>();
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);

            await foreach (var blob in container.GetBlobsAsync())
            {
                var blobDto = new BlobDto()
                {
                    Name = blob.Name,
                    FileUrl = container.Uri.AbsoluteUri + "/" + blob.Name,
                    ContentType = blob.Properties.ContentType
                };
                blobs.Add(blobDto);
            }
            return blobs;
        }

        public async Task<ContentDto> GetBlobFile(string name)
        {
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
            var blob = container.GetBlobClient(name);

            if (await blob.ExistsAsync())
            {
                var a = await blob.DownloadAsync();
                var contentDto = new ContentDto()
                {
                    Content = a.Value.Content,
                    ContentType = a.Value.ContentType,
                    Name = name
                };

                return contentDto;
            }

            return null;
        }

        public async Task<Stream> GetBlobFileStream(string name)
        {
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
            var blob = container.GetBlobClient(name);

            if (await blob.ExistsAsync())
            {
                var response = await blob.DownloadAsync();
                return response.Value.Content;

                //var response = await blob.DownloadAsync();
                //using (MemoryStream stream = new MemoryStream())                {
                //    response.Value.Content.CopyTo(stream);
                //    return stream;
            }
            else
            {
                return null;
            }
        }
    }
}


