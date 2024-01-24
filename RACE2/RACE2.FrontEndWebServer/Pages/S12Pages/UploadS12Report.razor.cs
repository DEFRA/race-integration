using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;
using System;
using System.Data;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadS12Report
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IReservoirService reservoirService { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public INotification _notificationService { get; set; } = default!;

        private bool FileUploadInProgress { get; set; } = false;
        private string _fileNameResult;
        public string fileExtn = String.Empty;
        private string UserName { get; set; } = "Unknown";
        private UserSpecificDto userDetails { get; set; }
        public string ReservoirName { get; set; } = default!;
        private IBrowserFile loadedFile;
        IBrowserFile selectedFile;
        List<IBrowserFile> selectedFiles;
        private UploadFileData UploadFileData { get; set; }=new UploadFileData();
        private List<FileUploadViewModel> fileUploadViewModels = new();
        DocumentDTO documentDTO = new DocumentDTO();
        [Parameter]
        public string ReservoirId { get; set; }
        [Parameter]
        public string ReservoirRegName { get; set; }
        [Parameter]
        public string UndertakerName { get; set; }
        [Parameter]
        public string UndertakerEmail { get; set; }
        [Parameter]
        public string SubmissionReference { get; set; }
        [Parameter]
        public string YesNoValue { get; set; }
        public Dictionary<string,string> ValidationErrors=new Dictionary<string,string>();

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateTask; // AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            userDetails = await userService.GetUserByEmailID(UserName);
            FileUploadInProgress = false;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            clearAllUploadFileSettings();
            await base.OnInitializedAsync();
        }
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles().ToList();
        }
        private async Task OnUploadSubmit()
        {
            clearAllUploadFileSettings();
            UploadFileData.EmptyFileSize = long.Parse(_config["EmptyFileSizeLimit"]);
            if (selectedFiles == null)
            {
                NoFileSelected();
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            else if (selectedFiles.Count() > 1)
            {
                MoreThanOneFileSelected();
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            else
            {
                selectedFile = selectedFiles[0];
                var fileExtns = selectedFile.Name.Split('.');
                int totalExtns = fileExtns.Length;
                fileExtn = selectedFile.Name.Split('.')[totalExtns - 1];
                var fileExtensionsAllowed = _config["SupportedUploadFileExtensions"].Split(';');
                if (!fileExtensionsAllowed.Contains(fileExtn))
                //if (!(fileExtn == "docx" || fileExtn == "doc" || fileExtn == "pdf"))
                {
                    WrongExtensionSelected();
                }
                if (selectedFile.Size > UploadFileData.MaxFileSize)
                {
                    MaxFileSizeExceeded();
                }
                if (selectedFile.Size < UploadFileData.EmptyFileSize)
                {
                    FileIsEmpty();                   
                }
                if (ValidationErrors.Count() > 0)
                {
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
                else
                {
                    try
                    {
                        //FileUploadInProgress = true;
                        //await InvokeAsync(() =>
                        //{
                        //    StateHasChanged();
                        //});

                        //var containerName = UserName.Split("@")[0];
                        //if (containerName.Contains('.'))
                        //{
                        //    containerName = containerName.Split('.')[0];
                        //}
                        var containerNameToUplodTo = _config["UnscannedContainer"];//"unscannedcontent";
                        //var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "."+ extn;
                        //var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + SubmissionReference + "." + fileExtn;
                        var trustedFileNameForFileStorage = SubmissionReference + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "." + fileExtn;
                        //Store the uploaded document information
                        documentDTO.FileName = selectedFile.Name.Split('.')[0];
                        documentDTO.FileType = fileExtn;
                        documentDTO.DateSent = DateTime.Now;
                        documentDTO.FileLocation = selectedFile.Name;
                        documentDTO.ReservoirId = Int32.Parse(ReservoirId);
                        documentDTO.SuppliedViaService = 1;
                        documentDTO.BlobStorageFileName = trustedFileNameForFileStorage;
                        documentDTO.DocumentType = "S12";
                        documentDTO.SuppliedBy = userDetails.Id;
                        var docID = await reservoirService.InsertUploadDocumentDetails(documentDTO);

                        var blobUrl = await blobStorageService.UploadFileToBlobAsync(containerNameToUplodTo, trustedFileNameForFileStorage, selectedFile.ContentType, selectedFile.OpenReadStream(UploadFileData.MaxFileSize));
                        if (blobUrl != null)
                        {
                            FileUploadViewModel fileUploadViewModel = new FileUploadViewModel()
                            {
                                FileName = trustedFileNameForFileStorage,
                                FileStorageUrl = blobUrl,
                                ContentType = selectedFile.ContentType,
                            };

                            fileUploadViewModels.Add(fileUploadViewModel);
                            SubmissionStatus updatedStatus = await reservoirService.UpdateReservoirStatus(Int32.Parse(ReservoirId), userDetails.Id, "Sent");
                            //_fileNameResult=await jsRuntime.InvokeAsync<string>("getFileName");

                            FileUploadInProgress = true;
                            await InvokeAsync(() =>
                            {
                                StateHasChanged();
                            });
                            //System.Threading.Thread.Sleep(20000);//wait for 20 seconds
                            var timeToWait = Int32.Parse(_config["TimeToWaitForUpload"]);
                            System.Threading.Thread.Sleep(timeToWait); //wait for timeToWait seconds

                            //For testing without virus scan, comment the next line and uncomment the next to next line
                            var containerNameToDownloadFrom = _config["CleanContainer"]; //"cleanfiles";
                            //var containerNameToDownloadFrom = _config["UnscannedContainer"];

                            var RSTEmailAddress = String.IsNullOrEmpty(_config["RSTEmailAddress"]) ? userDetails.Email : _config["RSTEmailAddress"];
                            var bytes = await blobStorageService.GetBlobAsByteArray(containerNameToDownloadFrom, trustedFileNameForFileStorage);
                            //if (bytes == null)
                            //{
                            //    System.Threading.Thread.Sleep(5000);//wait for 5 more seconds
                            //    bytes = await blobStorageService.GetBlobAsByteArray(containerNameToDownloadFrom, trustedFileNameForFileStorage);
                            //}
                            if (bytes != null)
                            {
                                await _notificationService.SendConfirmationMailtoSE(userDetails.Email, ReservoirRegName);
                                if (selectedFile.Size < UploadFileData.NotifyServiceFileAttachmentLimit)
                                {
                                    await _notificationService.SendConfirmationMailtoRST(RSTEmailAddress, ReservoirRegName, bytes, userDetails.cFirstName + " " + userDetails.cLastName, UndertakerName);
                                    if (YesNoValue == "Yes")
                                    {
                                        await _notificationService.SendConfirmationMailWithAttachment(bytes, UndertakerEmail, ReservoirRegName);
                                    }
                                }
                                else
                                {
                                    var internalEmail = _config["InternalEmailAddress"];
                                    await _notificationService.SendInternalMail(internalEmail, ReservoirRegName, UndertakerEmail, YesNoValue);
                                }
                                //Store the uploaded document information
                                documentDTO.SubmissionId = updatedStatus.Id;
                                await reservoirService.InsertDocumentRelatedTable(Int32.Parse(ReservoirId), updatedStatus.Id, docID);
                                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Information("File upload succeeded.");
                                goToNextPage();
                            }
                            else
                            {
                                // bool cleanFile = false;
                                bool notScanned = false;
                                var containerNameforMaliciousFile = _config["MaliciousContainer"]; //"maliciousfiles";
                                System.Threading.Thread.Sleep(timeToWait);
                                var maliciousbytes = await blobStorageService.GetBlobAsByteArray(containerNameforMaliciousFile, trustedFileNameForFileStorage);
                                //if (maliciousbytes == null)
                                //{
                                //    notScanned = true;                                 
                                //}
                                if (maliciousbytes != null)
                                {
                                    Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Fatal("File infected by virus.");
                                    FileVirusScanFailed();
                                    await InvokeAsync(() =>
                                    {
                                        StateHasChanged();
                                    });
                                    //var scanResult = await reservoirService.GetScannedResultbyDocId(docID);
                                    //if (scanResult.AVScanDate == DateTime.MinValue)
                                    //{
                                    //    notScanned = true;
                                    //}
                                    //else if (scanResult.AVScanDate != DateTime.MinValue && scanResult.CleanFileStorageLink != null && scanResult.IsClean)
                                    //{
                                    //    notScanned = false;
                                    //    cleanFile = true;
                                    //}
                                }
                                else
                                {
                                    notScanned = true;
                                }
                                if (notScanned)
                                {
                                    Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Fatal("File not scanned for virus.");
                                    FileUploadFailed();
                                    await InvokeAsync(() =>
                                    {
                                        StateHasChanged();
                                    });
                                }
                                //else if (!cleanFile)
                                //{
                                //    Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Fatal("File infected by virus.");
                                //    FileVirusScanFailed();
                                //    await InvokeAsync(() =>
                                //    {
                                //        StateHasChanged();
                                //    });
                                //}

                            }
                        }
                        else
                        {
                            Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Fatal("File upload failed.");
                            FileUploadFailed();
                            await InvokeAsync(() =>
                            {
                                StateHasChanged();
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Fatal("File upload failed : " + ex.Message);
                        FileUploadFailed();
                        await InvokeAsync(() =>
                        {
                            StateHasChanged();
                        });
                    }
                    finally
                    {
                    }
                }
            }
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = $"/send-your-statement/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}/{SubmissionReference}";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void goToNextPage()
        {
            bool forceLoad = false;
            string pagelink = $"/upload-confirmation/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}/{SubmissionReference}/{YesNoValue}";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void clearAllUploadFileSettings()
        {
            ValidationErrors.Clear();
        }
        private void MaxFileSizeExceeded()
        {
            ValidationErrors.Add("MaxFileSizeExceeded", "The selected file must be smaller than 30MB");
        }
        private void NoFileSelected()
        {
            ValidationErrors.Add("NoFileSelected", "Select a file");
        }
        private void MoreThanOneFileSelected()
        {
            ValidationErrors.Add("MoreThanOneFileSelected", "You can only select up to 1 file at the same time");
        }
        private void WrongExtensionSelected()
        {
            ValidationErrors.Add("WrongExtensionSelected", "The selected file must be a PDF or a Word document");
        }
        private void FileIsEmpty()
        {
            ValidationErrors.Add("FileIsEmpty", "The selected file is empty");
        }
        private void FileContainsVirus()
        {
            ValidationErrors.Add("FileContainsVirus", "The selected file contains a virus");
        }
        private void FilePasswordProtected()
        {
            ValidationErrors.Add("FilePasswordProtected", "FilePasswordProtected");
        }
        private void FileIncorrectTemplate()
        {
            ValidationErrors.Add("FileIncorrectTemplate", "FileIncorrectTemplate");
        }
        private void FileUploadFailed()
        {
            ValidationErrors.Add("FileUploadFailed", "The selected file could not be uploaded - try again");
        }

        private void FileVirusScanFailed()
        {
            ValidationErrors.Add("FileVirusScanFailed", "FileVirusScanFailed");
        }
    }
}

public class UploadFileData
{
    public long MaxFileSize { get; set; }
    public long NotifyServiceFileAttachmentLimit { get; set; }
    public long EmptyFileSize { get; set; }    
    public UploadFileData()
    {
        MaxFileSize = 30 * 1024 * 1024;
        //EmptyFileSize = 12 * 1024;
        NotifyServiceFileAttachmentLimit = 2 * 1024 * 1024;
    }
}
