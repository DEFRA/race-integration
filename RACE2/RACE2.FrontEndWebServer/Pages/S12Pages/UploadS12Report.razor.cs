using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

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
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        public string ReservoirName { get; set; } = default!;
        private IBrowserFile loadedFile;
        IBrowserFile selectedFile;
        private UploadFileData UploadFileData { get; set; }=new UploadFileData();
        private List<FileUploadViewModel> fileUploadViewModels = new();
        [Parameter]
        public string ReservoirId { get; set; }
        [Parameter]
        public string ReservoirRegName { get; set; }
        [Parameter]
        public string UndertakerName { get; set; }
        [Parameter]
        public string UndertakerEmail { get; set; }
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
            var userDetails = await userService.GetUserByEmailID(UserName);
            UserId = userDetails.Id;
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = UserId,
                Email = userDetails.Email
            };
            var rid = ReservoirId;
            var rname= ReservoirRegName;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            await base.OnInitializedAsync();
        }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            if (selectedFile.Size > UploadFileData.MaxFileSize)
            {
                UploadFileData.MaxFileSizeExceeded = true;
            }
            else
            {
                UploadFileData.MaxFileSizeExceeded = false;
            }
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        private async Task OnUploadSubmit()
        {
            try
            {
                var extn = selectedFile.Name.Split('.')[1];
                var containerName = UserName.Split("@")[0];
                if (containerName.Contains('.'))
                {
                    containerName = containerName.Split('.')[0];
                }
                var trustedFileNameForFileStorage = ReservoirRegName + "_S12_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "."+ extn;
                var blobUrl = await blobStorageService.UploadFileToBlobAsync(containerName,trustedFileNameForFileStorage, selectedFile.ContentType, selectedFile.OpenReadStream(UploadFileData.MaxFileSize));
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
                    SubmissionStatus updatedStatus = await reservoirService.UpdateReservoirStatus(Int32.Parse(ReservoirId), UserDetail.Id, "Sent");
                    goToNextPage();
                }
                else
                    warningMessage = "File Upload failed, Please try again!!";

            }
            catch (Exception ex)
            {
                warningMessage = "File Upload failed, Please try again!!";
            }

            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
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
            string pagelink = $"/upload-confirmation/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}/{YesNoValue}";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}

public class UploadFileData
{
    public long MaxFileSize { get; set; }
    public bool MaxFileSizeExceeded { get; set; }
    public bool noFileSelected { get; set; }
    public UploadFileData()
    {
        MaxFileSize = 30 * 1024 * 1024;
    }
}
