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

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadS12Report
    {
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

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateTask; // AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            userDetails = await userService.GetUserByEmailID(UserName);
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
                fileExtn = selectedFile.Name.Split('.')[1];
                if (!(fileExtn == "docx" || fileExtn == "pdf"))
                {
                    WrongExtensionSelected();
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
                else if (selectedFile.Size > UploadFileData.MaxFileSize)
                {
                    MaxFileSizeExceeded();
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
                else if (selectedFile.Size < UploadFileData.EmptyFileSize)
                {
                    FileIsEmpty();
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
                else
                {
                    try
                    {
                        var containerName = UserName.Split("@")[0];
                        if (containerName.Contains('.'))
                        {
                            containerName = containerName.Split('.')[0];
                        }
                        //var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "."+ extn;
                        var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + SubmissionReference + "." + fileExtn;

                        var blobUrl = await blobStorageService.UploadFileToBlobAsync(containerName, trustedFileNameForFileStorage, selectedFile.ContentType, selectedFile.OpenReadStream(UploadFileData.MaxFileSize));
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
                            var bytes = await blobStorageService.GetBlobAsByteArray(containerName, trustedFileNameForFileStorage);
                            await _notificationService.SendConfirmationMailtoSE(userDetails.Email, ReservoirRegName);
                            await _notificationService.SendConfirmationMailtoRST(userDetails.Email, ReservoirRegName, bytes, userDetails.cFirstName + " " + userDetails.cLastName, UndertakerName);
                            if (YesNoValue == "Yes")
                            {                     
                                await _notificationService.SendConfirmationMailWithAttachment(bytes, UndertakerEmail, ReservoirRegName);
                            }
                            //Store the uploaded document information
                            documentDTO.FileName = selectedFile.Name.Split('.')[0];
                            documentDTO.FileType = fileExtn;
                            documentDTO.DateSent = DateTime.Now;
                            documentDTO.FileLocation = selectedFile.Name;
                            documentDTO.ReservoirId = Int32.Parse(ReservoirId);
                            documentDTO.SuppliedViaService = 1;
                            documentDTO.SubmissionId = updatedStatus.Id;
                            documentDTO.DocumentType = "S12";
                            documentDTO.SuppliedBy = userDetails.Id;
                            await reservoirService.InsertUploadDocumentDetails(documentDTO);
                            Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "UploadS12Report OnUploadSubmit").Information("File upload succeeded.");
                            goToNextPage();
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
            string pagelink = $"/send-your-statement/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}";
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
            UploadFileData.NoFileSelected = false;
            UploadFileData.FileIsEmpty = false;
            UploadFileData.MaxFileSizeExceeded = false;
            UploadFileData.MoreThanOneFileSelected = false;
            UploadFileData.WrongExtensionSelected = false;
            UploadFileData.FileContainsVirus = false;
            UploadFileData.FileIncorrectTemplate = false;
            UploadFileData.FilePasswordProtected = false;
            UploadFileData.FileUploadFailed = false;     
        }
        private void MaxFileSizeExceeded()
        {
            clearAllUploadFileSettings();
            UploadFileData.MaxFileSizeExceeded = true;
        }
        private void NoFileSelected()
        {
            clearAllUploadFileSettings();
            UploadFileData.NoFileSelected = true;
        }
        private void MoreThanOneFileSelected()
        {
            clearAllUploadFileSettings();
            UploadFileData.MoreThanOneFileSelected = true;
        }
        private void WrongExtensionSelected()
        {
            clearAllUploadFileSettings();
            UploadFileData.WrongExtensionSelected = true;
        }
        private void FileIsEmpty()
        {
            clearAllUploadFileSettings();
            UploadFileData.FileIsEmpty = true;
        }
        private void FileContainsVirus()
        {
            clearAllUploadFileSettings();
            UploadFileData.FileContainsVirus = true;
        }
        private void FilePasswordProtected()
        {
            clearAllUploadFileSettings();
            UploadFileData.FilePasswordProtected = true;
        }
        private void FileIncorrectTemplate()
        {
            clearAllUploadFileSettings();
            UploadFileData.FileIncorrectTemplate = true;
        }
        private void FileUploadFailed()
        {
            clearAllUploadFileSettings();
            UploadFileData.FileUploadFailed = true;
        }
    }
}

public class UploadFileData
{
    public long MaxFileSize { get; set; }
    public long EmptyFileSize { get; set; }
    public bool MaxFileSizeExceeded { get; set; }
    public bool NoFileSelected { get; set; }
    public bool MoreThanOneFileSelected { get; set; }
    public bool WrongExtensionSelected { get; set; }
    public bool FileIsEmpty { get; set; }
    public bool FileContainsVirus { get; set; }
    public bool FilePasswordProtected { get; set; }
    public bool FileIncorrectTemplate { get; set; }
    public bool FileUploadFailed { get; set; }
    public UploadFileData()
    {
        MaxFileSize = 30 * 1024 * 1024;
        EmptyFileSize = 12 * 1024;
    }
}
