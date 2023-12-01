using nClam;
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.EventGrid;
using Azure;
using Azure.Storage.Blobs.Models;

namespace RACE2;

public class RunAvOnFileUploaded
{

    public RunAvOnFileUploaded()
    {

    }

    //public int ClamAVServerPort = int.Parse(Environment.GetEnvironmentVariable("CLAMAV_SERVER_PORT", EnvironmentVariableTarget.Process) ?? throw new Exception("Port must be configured"));
    //public string ConnectionString = Environment.GetEnvironmentVariable("ATTACHMENT_STORAGE_CONNSTRING", EnvironmentVariableTarget.Process) ?? throw new Exception("Connection string missing");
    //public string ClamAVServerUrl = Environment.GetEnvironmentVariable("CLAMAV_SERVER_URL", EnvironmentVariableTarget.Process) ?? throw new Exception("Connection string missing");

    public int ClamAVServerPort = 3310;
    public string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=pocacinfst1401;AccountKey=M3CuH8gcrjr03wsUXsPcLvV9OWyHxhuCdUxF4iuMbqjqnLSpaCZLOkfkWlNMjHtxhAVvDmdWNb+0+AStOQ04Nw==;EndpointSuffix=core.windows.net";
    //public string ClamAVServerUrl = "http://20.50.147.7";
    public string ClamAVServerUrl = "https://clamav.orangedesert-f440d294.uksouth.azurecontainerapps.io";
    public const string CREATED_EVENT_URL = "url";

    private bool TryGetFileNameAndContainerFromUrl(string url, [NotNullWhen(true)] out string? fileName, [NotNullWhen(true)] out string? container)
    {
        var urlParts = url.Split('/');
        var urlFileName = urlParts.Last();
        fileName = string.IsNullOrEmpty(urlFileName) ? null : urlFileName;
        container = urlParts.SkipLast(1).Last();
        bool isValid = !String.IsNullOrWhiteSpace(fileName) && !String.IsNullOrWhiteSpace(container);
        return isValid;
    }

    private bool TryGetEventData<T>(Microsoft.Azure.EventGrid.Models.EventGridEvent eventGridEvent, string property, [NotNullWhen(true)] out T? value)
    {
        value = default(T);
        var eventData = eventGridEvent.Data.ToString();
        if (eventData == null) return false;
        var eventDataJson = JObject.Parse(eventData);
        bool found = eventDataJson.TryGetValue(property, out var eventDataValue);
        value = found ? eventDataValue!.Value<T>() : value;
        return found;
    }


    [FunctionName("RunAvOnFileUploaded")]
    public async Task Run([EventGridTrigger] Microsoft.Azure.EventGrid.Models.EventGridEvent eventGridEvent, ILogger log)
    {
        // ##### READ EVENT GRID EVENT FROM REQUEST #####
        log.LogInformation("Begin handling request. {EventGridEvent}", eventGridEvent.Data);

        if (!TryGetEventData<string>(eventGridEvent, CREATED_EVENT_URL, out var url))
        {
            log.LogError("EventData could not be parsed. UrlFound check failed.");
            return;
        }

        // ##### READ FILE NAME AND STORAGE ACCOUNT CONTAINER NAME FROM EVENT GRID EVENT
        if (!TryGetFileNameAndContainerFromUrl(url, out var fileName, out var container))
        {
            log.LogError("Invalid file URI, could not process. Url value: {Url}", url);
            return;
        }

        // ##### DOWNLOAD FILE FROM AZURE STORAGE #####
        log.LogInformation("Downloading {FileName} from container {Container}...", fileName, container);
        var blobClient = new BlobClient(ConnectionString, container, fileName);

        // GOOD ADDITIONAL CHECKS TO PERFORM HERE:
        //- Check that the file isn't already scanned (use a metadata to track that) or that the last scan time was long enough ago
        //- Check that the file size is not too big to a configured maximum
        //- Check that the file extension and mimetype against an allowlist

        // ##### DOWNLOAD FILE #####
        using var ms = new MemoryStream();
        await blobClient.DownloadToAsync(ms);

        // GOOD ADDITIONAL CHECKS TO PERFORM HERE:
        //- Check that the first few bytes (File magic numbers, or headers) of the file match the binary that the extension implies

        // ##### RUN AV ON FILE #####
        log.LogInformation("Download completed. Connecting to AV server {ClamAVServerUrl}:{ClamAVServerPort}... ", ClamAVServerUrl, ClamAVServerPort);
        var clam = new ClamClient(ClamAVServerUrl, ClamAVServerPort);
        bool isConnected = await clam.PingAsync();
        var version = await clam.GetVersionAsync();
        if (!isConnected) throw new Exception("AV server connection could not be established.");

        log.LogInformation("Connection ok: {IsConnected}. AV server reports version: {ServerVersion}", isConnected, version);

        var scanResult = await clam.SendAndScanFileAsync(ms.ToArray());

        switch (scanResult.Result)
        {
            case ClamScanResults.Clean:
                log.LogInformation("The file is clean!");
                // You could save the file to storage here, or send event grid events for a success or perform HTTP requests accordingly
                // You might also want to write to the file's metadata, that you have performed a check on it and the time when the check was performed
                break;
            case ClamScanResults.VirusDetected:
                log.LogCritical("Virus found. Deleting file.");
                await blobClient.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);
                // You might also notify other systems here about the faulty file
                break;
            case ClamScanResults.Error:
            default:
                log.LogInformation("File scan error occurred. {ScanResult}", scanResult.RawResult);
                break;
        }
    }
}
