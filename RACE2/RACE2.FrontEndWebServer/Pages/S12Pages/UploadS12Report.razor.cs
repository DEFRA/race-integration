using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadS12Report
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        private string warningMessage = "";
        private string displayMessage = "";
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 3;
        private bool fileLoading;
        string Message = "No file(s) selected";
        IReadOnlyList<IBrowserFile> selectedFiles;
        private List<FileUploadViewModel> fileUploadViewModels = new();

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            var userDetails = await userService.GetUserByEmailID(UserName);
            UserId = userDetails.Id;
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = UserId,
                Email = userDetails.Email
            };
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
        }
        private async void OnUploadSubmit()
        {
            fileLoading = true;
            foreach (var file in selectedFiles)
            {
                try
                {
                    var extn = file.Name.Split('.')[1];
                    var containerName = UserName.Split("@")[0];
                    if (containerName.Contains('.'))
                    {
                        containerName = containerName.Split('.')[0];
                    }
                    var trustedFileNameForFileStorage = "S12Report_"+ CurrentReservoir.PublicName+ "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "."+ extn;
                    var blobUrl = await blobStorageService.UploadFileToBlobAsync(containerName,trustedFileNameForFileStorage, file.ContentType, file.OpenReadStream(20971520));
                    if (blobUrl != null)
                    {
                        FileUploadViewModel fileUploadViewModel = new FileUploadViewModel()
                        {
                            FileName = trustedFileNameForFileStorage,
                            FileStorageUrl = blobUrl,
                            ContentType = file.ContentType,
                        };

                        fileUploadViewModels.Add(fileUploadViewModel);
                        displayMessage = trustedFileNameForFileStorage + " Uploaded!!";
                    }
                    else
                        warningMessage = "File Upload failed, Please try again!!";

                }
                catch (Exception ex)
                {
                    warningMessage = "File Upload failed, Please try again!!";
                }
            }

            fileLoading = false;
            this.StateHasChanged();
        }

        private async void OnFileDeleteClick(FileUploadViewModel attachment)
        {
            try
            {
                var containerName = UserName.Split("@")[0];
                if (containerName.Contains('.'))
                {
                    containerName = containerName.Split('.')[0];
                }
                var deleteResponse = await blobStorageService.DeleteFileToBlobAsync(containerName, attachment.FileName);
                if (deleteResponse)
                {
                    fileUploadViewModels.Remove(attachment);
                    displayMessage = attachment.FileName + " Deleted!!";
                }

            }
            catch (Exception)
            {
                warningMessage = "Something went wrong! Please try again.";
            }
            this.StateHasChanged();
        }
        private async void OnFileViewClick(FileUploadViewModel attachment)
        {
            try
            {
                var containerName = UserName.Split("@")[0];
                if (containerName.Contains('.'))
                {
                    containerName = containerName.Split('.')[0];
                }
                var sasToken = await blobStorageService.GetBlobAsTokenByFile(containerName,attachment.FileName);
                if (sasToken != null)
                {
                    string fileUrl = attachment.FileStorageUrl + "?" + sasToken;
                    await jsRuntime.InvokeAsync<object>("open", fileUrl, "_blank");
                }

            }
            catch (Exception)
            {
                warningMessage = "Something went wrong! Please try again.";
            }
            this.StateHasChanged();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }        
    }
}
