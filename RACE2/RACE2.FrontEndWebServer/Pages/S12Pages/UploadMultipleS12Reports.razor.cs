using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;
using System;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadMultipleS12Reports
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        private string warninngMessage = "";
        private string displayMessage = "";
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 3;
        private bool fileLoading;
        string Message = "No file(s) selected";
        IReadOnlyList<IBrowserFile> selectedFiles;
        private List<FileUploadViewModel> fileUploadViewModels = new();
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask; // AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            var userDetails = await userService.GetUserByEmailID(UserName);
            UserId = userDetails.Id;
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = UserId,
                Email = userDetails.Email,
                cFirstName = userDetails.cFirstName,
                cLastName = userDetails.cLastName
            };
            CurrentReservoir = new Reservoir();

            base.OnInitialized();
        }
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
         }
        private async void OnUploadSubmit()
        {
            fileLoading = true;
            foreach (var file in selectedFiles)
            {
                try
                {
                    var extn = file.Name.Split('.')[1];
                    var containerName = UserDetail.cFirstName.ToLower() + UserDetail.cLastName.ToLower();
                    var trustedFileNameForFileStorage = "S12Report_" + CurrentReservoir.PublicName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "." + extn;
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
                        warninngMessage = "File Upload failed, Please try again!!";

                }
                catch (Exception ex)
                {
                    warninngMessage = "File Upload failed, Please try again!!";
                }
            }

            fileLoading = false;
        }

        private async void OnFileDeleteClick(FileUploadViewModel attachment)
        {
            try
            {
                var containerName = UserDetail.cFirstName.ToLower() + UserDetail.cLastName.ToLower();
                var deleteResponse = await blobStorageService.DeleteFileToBlobAsync(containerName, attachment.FileName);
                if (deleteResponse)
                {
                    fileUploadViewModels.Remove(attachment);
                    displayMessage = attachment.FileName + " Deleted!!";
                }

            }
            catch (Exception)
            {
                warninngMessage = "Something went wrong! Please try again.";
            }
        }
        private async void OnFileViewClick(FileUploadViewModel attachment)
        {
            try
            {
                var containerName = UserDetail.cFirstName.ToLower() + UserDetail.cLastName.ToLower();
                var sasToken = await blobStorageService.GetBlobAsTokenByFile(containerName, attachment.FileName);
                if (sasToken != null)
                {
                    string fileUrl = attachment.FileStorageUrl + "?" + sasToken;
                    await jsRuntime.InvokeAsync<object>("open", fileUrl, "_blank");
                }

            }
            catch (Exception)
            {
                warninngMessage = "Something went wrong! Please try again.";
            }
        }

    private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }        
    }
}
