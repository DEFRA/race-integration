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

        public string displayMessage { get; set; }

        public string warningMessage { get; set; }

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
            UploadFileData.NoFileSelected = false;
            UploadFileData.MoreThanOneFileSelected = false;
            UploadFileData.WrongExtensionSelected = false;
            UploadFileData.MaxFileSizeExceeded = false;
            UploadFileData.FileContainsVirus = false;
            UploadFileData.FileIncorrectTemplate = false;
            UploadFileData.FilePasswordProtected = false;
            UploadFileData.FileIsEmpty = false;
            UploadFileData.FileUploadFailed = false;
            UploadFileData.FileInfectedWithVirus = false;

            await base.OnInitializedAsync();
        }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles().ToList();

            if (selectedFiles.Count() > 1)
            {
                UploadFileData.MoreThanOneFileSelected = true;
                UploadFileData.NoFileSelected = false;
                UploadFileData.WrongExtensionSelected = false;
                UploadFileData.MaxFileSizeExceeded = false;
                UploadFileData.FileContainsVirus = false;
                UploadFileData.FileIncorrectTemplate = false;
                UploadFileData.FilePasswordProtected = false;
                UploadFileData.FileIsEmpty = false;
                UploadFileData.FileUploadFailed = false;
                UploadFileData.FileInfectedWithVirus = false;
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            else
            {
                selectedFile = selectedFiles[0];
                var fileExtn = selectedFile.Name.Split('.')[1];
                if (!(fileExtn == "docx" || fileExtn == "pdf"))
                {
                    UploadFileData.WrongExtensionSelected = true;
                    UploadFileData.MaxFileSizeExceeded = false;
                    UploadFileData.MoreThanOneFileSelected = false;
                    UploadFileData.NoFileSelected = false;
                    UploadFileData.FileContainsVirus = false;
                    UploadFileData.FileIncorrectTemplate = false;
                    UploadFileData.FilePasswordProtected = false;
                    UploadFileData.FileIsEmpty = false;
                    UploadFileData.FileUploadFailed = false;
                    UploadFileData.FileInfectedWithVirus = false;
                }
                else if (selectedFile.Size > UploadFileData.MaxFileSize)
                {
                    UploadFileData.MaxFileSizeExceeded = true;
                    UploadFileData.MoreThanOneFileSelected = false;
                    UploadFileData.NoFileSelected = false;
                    UploadFileData.WrongExtensionSelected = false;
                    UploadFileData.FileContainsVirus = false;
                    UploadFileData.FileIncorrectTemplate = false;
                    UploadFileData.FilePasswordProtected = false;
                    UploadFileData.FileIsEmpty = false;
                    UploadFileData.FileUploadFailed = false;
                    UploadFileData.FileInfectedWithVirus = false;
                }
                else if (selectedFile.Size < UploadFileData.EmptyFileSize)
                {
                    UploadFileData.FileIsEmpty = true;
                    UploadFileData.MaxFileSizeExceeded = false;
                    UploadFileData.MoreThanOneFileSelected = false;
                    UploadFileData.NoFileSelected = false;
                    UploadFileData.WrongExtensionSelected = false;
                    UploadFileData.FileContainsVirus = false;
                    UploadFileData.FileIncorrectTemplate = false;
                    UploadFileData.FilePasswordProtected = false;
                    UploadFileData.FileUploadFailed = false;
                    UploadFileData.FileInfectedWithVirus = false;
                }
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
        }
        private async Task OnUploadSubmit()
        {
            if (selectedFiles != null)
            {
                try
                {
                    var extn = selectedFile.Name.Split('.')[1];
                    var containerName = UserName.Split("@")[0];
                    if (containerName.Contains('.'))
                    {
                        containerName = containerName.Split('.')[0];
                    }
                    //var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "."+ extn;
                    var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + SubmissionReference + "." + extn;

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
                        displayMessage = trustedFileNameForFileStorage + " Uploaded!!";
                        SubmissionStatus updatedStatus = await reservoirService.UpdateReservoirStatus(Int32.Parse(ReservoirId), userDetails.Id, "Sent");
                        //_fileNameResult=await jsRuntime.InvokeAsync<string>("getFileName");
                        var bytes = await blobStorageService.GetBlobAsByteArray(containerName, trustedFileNameForFileStorage);
                        //var bytes = new byte[selectedFile.Size];
                        await _notificationService.SendConfirmationMailtoSE(userDetails.Email, ReservoirRegName);
                        await _notificationService.SendConfirmationMailtoRST(userDetails.Email, ReservoirRegName, bytes, userDetails.cFirstName + " " + userDetails.cLastName, UndertakerName);
                        if (YesNoValue == "Yes")
                        {
                            //await selectedFile.OpenReadStream(selectedFile.Size).ReadAsync(bytes);                        
                            await _notificationService.SendConfirmationMailWithAttachment(bytes, UndertakerEmail, ReservoirRegName);
                        }
                        //Store the uploaded document information
                        documentDTO.FileName = selectedFile.Name.Split('.')[0];
                        documentDTO.FileType = extn;
                        documentDTO.DateSent = DateTime.Now;
                        documentDTO.FileLocation = selectedFile.Name;
                        documentDTO.ReservoirId = Int32.Parse(ReservoirId);
                        documentDTO.SuppliedViaService = 1;
                        documentDTO.SubmissionId = updatedStatus.Id;
                        documentDTO.DocumentType = "S12";
                        documentDTO.SuppliedBy = userDetails.Id;
                        await reservoirService.InsertUploadDocumentDetails(documentDTO);
                        goToNextPage();
                    }
                    else
                    {
                        warningMessage = "File Upload failed, Please try again!!";
                        UploadFileData.FileUploadFailed = true;
                        UploadFileData.NoFileSelected = false;
                        UploadFileData.FileIsEmpty = false;
                        UploadFileData.MaxFileSizeExceeded = false;
                        UploadFileData.MoreThanOneFileSelected = false;
                        UploadFileData.WrongExtensionSelected = false;
                        UploadFileData.FileContainsVirus = false;
                        UploadFileData.FileIncorrectTemplate = false;
                        UploadFileData.FilePasswordProtected = false;
                        UploadFileData.FileInfectedWithVirus = false;
                    }
                }
                catch (Exception ex)
                {
                    warningMessage = "File Upload failed, Please try again!!";
                    UploadFileData.FileUploadFailed = true;
                    UploadFileData.NoFileSelected = false;
                    UploadFileData.FileIsEmpty = false;
                    UploadFileData.MaxFileSizeExceeded = false;
                    UploadFileData.MoreThanOneFileSelected = false;
                    UploadFileData.WrongExtensionSelected = false;
                    UploadFileData.FileContainsVirus = false;
                    UploadFileData.FileIncorrectTemplate = false;
                    UploadFileData.FilePasswordProtected = false;
                    UploadFileData.FileInfectedWithVirus = false;
                }
                finally
                {
                    await InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
            }
            else
            {
                UploadFileData.NoFileSelected = true;
                UploadFileData.FileIsEmpty = false;
                UploadFileData.MaxFileSizeExceeded = false;
                UploadFileData.MoreThanOneFileSelected = false;
                UploadFileData.WrongExtensionSelected = false;
                UploadFileData.FileContainsVirus = false;
                UploadFileData.FileIncorrectTemplate = false;
                UploadFileData.FilePasswordProtected = false;
                UploadFileData.FileUploadFailed = false;
                UploadFileData.FileInfectedWithVirus = false;
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
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
    public bool FileInfectedWithVirus { get; set; }
    public UploadFileData()
    {
        MaxFileSize = 30 * 1024 * 1024;
        EmptyFileSize = 12 * 1024;
    }
}
