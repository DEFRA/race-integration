﻿using Azure.Identity;
using Azure.Messaging.EventGrid;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionEventTrigger
{
    public static class MoveMaliciousBlobEventTrigger
    {
        private const string AntimalwareScanEventType = "Microsoft.Security.MalwareScanningResult";
        private const string MaliciousVerdict = "Malicious";
        private const string CleanVerdict = "No threats found";
        private const string MalwareContainer = "maliciousfiles";
        private const string CleanContainer = "cleanfiles";
        private const string InterestedContainer = "unscannedcontent";


        [FunctionName("MoveMaliciousBlobEventTrigger")]
        public static async Task RunAsync([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            string connString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);

            if (eventGridEvent.EventType != AntimalwareScanEventType)
            {
                log.LogInformation("Event type is not an {0} event, event type:{1}", AntimalwareScanEventType, eventGridEvent.EventType);
                return;
            }

            var storageAccountName = eventGridEvent?.Subject?.Split("/")[^1];
            log.LogInformation("Received new scan result for storage {0}", storageAccountName);
            var eventData = JsonDocument.Parse(eventGridEvent.Data).RootElement;
            var verdict = eventData.GetProperty("scanResultType").GetString();
            var blobUriString = eventData.GetProperty("blobUri").GetString();
            var blobUri = new Uri(blobUriString);
            var blobUriBuilder = new BlobUriBuilder(blobUri);
            var scannedTime = eventData.GetProperty("scanFinishedTimeUtc").GetString();
           // DateTime scannedDataTime = new DateTime(scannedTime);
            bool isclean = true;
            //string blobpath;
            string documentName = string.Empty;

            // Filter events from interested containers
            if (blobUriBuilder.BlobContainerName != InterestedContainer)
            {
                log.LogInformation("Event is not from the interested containers, ignoring");
                return;
            }

            if (verdict == null || blobUriString == null)
            {
                log.LogError("Event data doesn't contain 'verdict' or 'blobUri' fields");
                throw new ArgumentException("Event data doesn't contain 'verdict' or 'blobUri' fields");
            }

            if (verdict == MaliciousVerdict)
            {
                log.LogInformation("blob {0} is malicious, moving it to {1} container", blobUri, MalwareContainer);
                try
                {
                    await MoveMaliciousBlobAsync(blobUri, log,false, Convert.ToDateTime(scannedTime),connString);
                }
                catch (Exception e)
                {
                    log.LogError(e, "Can't move blob to container '{0}'", MalwareContainer);
                    throw;
                }
            }

            if (verdict == CleanVerdict)
            {
                log.LogInformation("blob {0} is malicious, moving it to {1} container", blobUri, CleanContainer);
                try
                {
                    await MoveCleanBlobAsync(blobUri, log,isclean,Convert.ToDateTime(scannedTime),connString);
                   
                }
                catch (Exception e)
                {
                    log.LogError(e, "Can't move blob to container '{0}'", CleanContainer);
                    throw;
                }
            }
        }

        private static async Task MoveMaliciousBlobAsync(Uri blobUri, ILogger log, bool IsClean, DateTime scannedTime, string connString)

        {
            var blobUriBuilder = new BlobUriBuilder(blobUri);
            if (blobUriBuilder.BlobContainerName == MalwareContainer)
            {
                log.LogInformation("blob {0} is already in {1} container, skipping", blobUriBuilder.BlobName, MalwareContainer);
                return;
            }
            var destContainerUri = new Uri($"https://{blobUriBuilder.Host}/{MalwareContainer}");
            var defaultAzureCredential = new DefaultAzureCredential();
            var srcBlobClient = new BlobClient(blobUri, defaultAzureCredential);
            var destContainerClient = new BlobContainerClient(destContainerUri, defaultAzureCredential);
            log.LogInformation("Creating {0} container if it doesn't exist", MalwareContainer);
            await destContainerClient.CreateIfNotExistsAsync();
            var destBlobClient = destContainerClient.GetBlobClient(blobUriBuilder.BlobName);

            if (!await srcBlobClient.ExistsAsync())
            {
                log.LogError("blob {0} doesn't exist", blobUri);
                return;
            }

            log.LogInformation("MoveBlob: Copying blob to {0}", destBlobClient.Uri);
            var copyFromUriOperation = await destBlobClient.StartCopyFromUriAsync(srcBlobClient.Uri);
            await copyFromUriOperation.WaitForCompletionAsync();
            log.LogInformation("MoveBlob: Deleting source blob {0}", srcBlobClient.Uri);
            await srcBlobClient.DeleteAsync();
            log.LogInformation("MoveBlob: blob moved successfully");
            log.LogInformation("Updating the database");
            log.LogInformation("{0}", destBlobClient.Uri);
            log.LogInformation("{0}", blobUriBuilder.BlobName);
           // log.LogInformation("{0}", connString);
            log.LogInformation("{0}", scannedTime);
            UpdateScanResulttoDB(connString, scannedTime, IsClean, Convert.ToString(destBlobClient.Uri), blobUriBuilder.BlobName);
            log.LogInformation("Database updated successfully");
        }

        private static async Task MoveCleanBlobAsync(Uri blobUri, ILogger log,bool IsClean,DateTime scannedTime,string connString)
        {
            var blobUriBuilder = new BlobUriBuilder(blobUri);
            if (blobUriBuilder.BlobContainerName == CleanContainer)
            {
                log.LogInformation("blob {0} is already in {1} container, skipping", blobUriBuilder.BlobName, CleanContainer);
                return;
            }
            var destContainerUri = new Uri($"https://{blobUriBuilder.Host}/{CleanContainer}");
            var defaultAzureCredential = new DefaultAzureCredential();
            var srcBlobClient = new BlobClient(blobUri, defaultAzureCredential);
            var destContainerClient = new BlobContainerClient(destContainerUri, defaultAzureCredential);
            log.LogInformation("Creating {0} container if it doesn't exist", CleanContainer);
            await destContainerClient.CreateIfNotExistsAsync();
            var destBlobClient = destContainerClient.GetBlobClient(blobUriBuilder.BlobName);

            if (!await srcBlobClient.ExistsAsync())
            {
                log.LogError("blob {0} doesn't exist", blobUri);
                return;
            }

            log.LogInformation("MoveBlob: Copying blob to {0}", destBlobClient.Uri);
            var copyFromUriOperation = await destBlobClient.StartCopyFromUriAsync(srcBlobClient.Uri);
            await copyFromUriOperation.WaitForCompletionAsync();
            log.LogInformation("MoveBlob: Deleting source blob {0}", srcBlobClient.Uri);
            await srcBlobClient.DeleteAsync();
            log.LogInformation("MoveBlob: blob moved successfully");
            log.LogInformation("Updating the database");
            log.LogInformation("{0}", destBlobClient.Uri);
            log.LogInformation("{0}", blobUriBuilder.BlobName);
           // log.LogInformation("{0}", connString);
            log.LogInformation("{0}", scannedTime);
            UpdateScanResulttoDB(connString, scannedTime, IsClean, Convert.ToString(destBlobClient.Uri), blobUriBuilder.BlobName);
            log.LogInformation("Database updated successfully");
        }

        private static void UpdateScanResulttoDB(string connString, DateTime scannedTime, bool isclean,string blobpath,string documentName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("sp_UpdateScannedDocumentResult", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@scannedtime", scannedTime));
                cmd.Parameters.Add(new SqlParameter("@isClean", isclean));
                cmd.Parameters.Add(new SqlParameter("@uploadBlobpath", blobpath));
                cmd.Parameters.Add(new SqlParameter("@documentName", documentName));

              cmd.ExecuteNonQuery();
                conn.Close();
               
            }
        }
    }
}