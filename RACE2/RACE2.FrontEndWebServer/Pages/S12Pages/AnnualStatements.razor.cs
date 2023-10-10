using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class AnnualStatements
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IOpenXMLUtilitiesService openXMLUtilitiesService { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IReservoirService reservoirService { get; set; } = default!;
        bool? IsLoggedIn;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();
        private List<ReservoirDetailsDTO> ReservoirDetailsLinkedToUser { get; set; } = new List<ReservoirDetailsDTO>();
        private List<ReservoirsLinkedToUserForDisplay> ReservoirsLinkedToUserForDisplay { get; set; } =new List<ReservoirsLinkedToUserForDisplay>();
        private List<ReservoirsLinkedToUserForDisplay> ReservoirsLinkedToUserForDisplayOnStart { get; set; } = new List<ReservoirsLinkedToUserForDisplay>();
        private IEnumerable<Claim> Claims { get; set; }
        private List<OperatorDTO> Undertakers { get; set; }

        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();
        CultureInfo en = CultureInfo.GetCultureInfo("en-US");
        int selectedReservoirId= 0;

        //We need a field to tell us which direction the table is currently sorted by
        private bool IsSortedAscending;

        //We also need a field to tell us which column the table is sorted by.
        private string CurrentSortColumn;

        UserSpecificDto userDetails { get; set; }
        List<SubmissionStatusDTO> SubmissionStatusList { get; set; }
        SubmissionStatusDTO SubmissionStatus { get; set; }

    protected async override Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                PhoneNumber = userDetails.PhoneNumber,
                c_first_name = userDetails.c_first_name,
                c_last_name = userDetails.c_last_name,
                c_IsFirstTimeUser = userDetails.c_IsFirstTimeUser
            };
            if (UserDetail.c_IsFirstTimeUser)
            {
                bool forceLoad = true;
                Uri pagelink = new Uri(_config["RACE2SecurityProviderURL"] + "/Identity/Account/CreatePassword?userEmail="+ UserName);
                NavigationManager.NavigateTo(pagelink.ToString());
            }
            ReservoirDetailsLinkedToUser = await reservoirService.GetReservoirsByUserId(UserDetail.Id);

            foreach (var rn in ReservoirDetailsLinkedToUser)
            {
                var r = new Reservoir()
                {
                    Id = rn.Id,
                    RaceReservoirId = rn.RaceReservoirId,
                    PublicName = rn.PublicName,
                    NearestTown = rn.NearestTown,
                    GridReference = rn.GridReference,
                    OperatorType = rn.OperatorType
                };
                r.Address = new Address()
                {
                    AddressLine1 = rn.Address.AddressLine1,
                    AddressLine2 = rn.Address.AddressLine2,
                    Town = rn.Address.Town,
                    County = rn.Address.County,
                    Postcode = rn.Address.Postcode
                };
                ReservoirsLinkedToUser.Add(r);
            }
            var actionUserDetail = new StoreUserDetailAction(UserDetail);
            Dispatcher.Dispatch(actionUserDetail);

            var actionReservoirsLinkedToUser = new StoreUserReservoirsAction(ReservoirsLinkedToUser);
            Dispatcher.Dispatch(actionReservoirsLinkedToUser);
            PopulateReservoirsToDisplay(ReservoirsLinkedToUser);
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitializedAsync();
        }

        private async void PopulateReservoirsToDisplay(List<Reservoir> reservoirs)
        {
            foreach (var reservoir in reservoirs)
            {
                Undertakers = await reservoirService.GetOperatorsforReservoir(reservoir.Id, reservoir.OperatorType);
                ReservoirsLinkedToUserForDisplay reservoirsLinkedToUser=new ReservoirsLinkedToUserForDisplay();
                reservoirsLinkedToUser.ReservoirName = reservoir.PublicName;
                if (Undertakers != null && Undertakers.Count() > 0)
                {
                    if (!String.IsNullOrEmpty(Undertakers[0].OrgName))
                        reservoirsLinkedToUser.UndertakerName = Undertakers[0].OrgName;
                    else if (!String.IsNullOrEmpty(Undertakers[0].OperatorFirstName))
                        reservoirsLinkedToUser.UndertakerName = Undertakers[0].OperatorFirstName + " " + Undertakers[0].OperatorLastName;
                    else
                        reservoirsLinkedToUser.UndertakerName = "";
                }
                SubmissionStatusList = await reservoirService.GetReservoirStatusByUserId(UserDetail.Id);
                SubmissionStatus = SubmissionStatusList.Where(s => s.PublicName == reservoir.PublicName).FirstOrDefault();
                reservoirsLinkedToUser.DueDate = SubmissionStatus.DueDate!=DateTime.MinValue? SubmissionStatus.DueDate.ToString("dd MMMMM yyyy") :"";
                reservoirsLinkedToUser.Status = SubmissionStatus.Status!=null? SubmissionStatus.Status:"Not Started";
                ReservoirsLinkedToUserForDisplay.Add(reservoirsLinkedToUser);
            }
            ReservoirsLinkedToUserForDisplayOnStart = ReservoirsLinkedToUserForDisplay;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        protected override async void OnAfterRender(bool firstRender)
        {

        }
        
        private async void DownloadReportTemplate(ReservoirsLinkedToUserForDisplay item)
        {
            var reservoir= ReservoirsLinkedToUser.Where(r=>r.PublicName==item.ReservoirName).FirstOrDefault();
            SubmissionStatus = SubmissionStatusList.Where(s => s.PublicName == reservoir.PublicName).FirstOrDefault();
            //var undertaker=Undertakers.Where(u=>u.ReservoirId==reservoir.Id).FirstOrDefault();
            var blobName = SubmissionStatus.override_template + ".docx";// "S12ReportTemplate.docx";
            
            Stream response = await blobStorageService.GetBlobFileStream(blobName);
            S12PrePopulationFields s12PrePopulationFields = new S12PrePopulationFields();
            s12PrePopulationFields.ReservoirName = reservoir.PublicName;
            s12PrePopulationFields.ReservoirNearestTown = reservoir.NearestTown != null ? reservoir.NearestTown : "";
            s12PrePopulationFields.ReservoirGridRef = reservoir.GridReference != null ? reservoir.GridReference : "";
            s12PrePopulationFields.SupervisingEngineerName = UserDetail.c_first_name + " " + UserDetail.c_last_name;
            Address address = userDetails.addresses.FirstOrDefault();
            s12PrePopulationFields.SupervisingEngineerAddress = address.AddressLine1+", "+ address.AddressLine2 + ", " + address.Town + ", " + address.County + ", " + address.Postcode;
            s12PrePopulationFields.SupervisingEngineerEmail = UserDetail.Email;
            s12PrePopulationFields.SupervisingEngineerPhoneNumber = UserDetail.PhoneNumber != null ? UserDetail.PhoneNumber : "";

            MemoryStream processedStream = openXMLUtilitiesService.SearchAndReplace(response, s12PrePopulationFields);
            processedStream.Position = 0;
            var streamRef = new DotNetStreamReference(stream: processedStream);
            await jsRuntime.InvokeVoidAsync("downloadFileFromStream", blobName, streamRef);
            SubmissionStatus updatedStatus = await reservoirService.UpdateReservoirStatus(reservoir.Id,UserDetail.Id);
            var reservoirLinkedToUser = ReservoirsLinkedToUserForDisplay.Where(r => r.ReservoirName == reservoir.PublicName).FirstOrDefault();
            reservoirLinkedToUser.Status = updatedStatus.Status;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        private void DownloadTemplateFile(ReservoirsLinkedToUserForDisplay Item)
        {
            DownloadReportTemplate(Item);
        }
        public async void GoToNextPage()
        {
            var u = CurrentUserDetailState.Value.CurrentUserDetail;
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void gotoPage(SubmissionStatusDTO reservoirStatus)
        {
            var reservoir = ReservoirsLinkedToUser.Where(s => s.PublicName == reservoirStatus.PublicName).FirstOrDefault();
            var action = new StoreReservoirAction(reservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            if (reservoirStatus.Status.ToUpper() == "DRAFT SENT")
            {
                pagelink = "/s12-statement-confirmation-draft-sent";
            }
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void gotoSubmissionPage(SubmissionStatusDTO reservoirStatus)
        {
            var reservoir = ReservoirsLinkedToUser.Where(s => s.PublicName == reservoirStatus.PublicName).FirstOrDefault();
            var action = new StoreReservoirAction(reservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/s12-statement-confirmation";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        
        private async void SearchOnEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" ||  e.Code == "NumpadEnter")
            {
                FilterData(_searchString);
                //if (!string.IsNullOrEmpty(_searchString))
                //{
                //    ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplay.Where(r => r.ReservoirName.ToLower().Contains(_searchString.ToLower())).ToList();
                //    await InvokeAsync(() =>
                //    {
                //        StateHasChanged();
                //    });
                //}
                //else
                //{
                //    ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplayOnStart;
                //}
            }           
        }
        private async void FilterData(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplay.Where(r => r.ReservoirName.ToLower().Contains(searchTerm.ToLower())).ToList();
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            else
            {
                ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplayOnStart;
            }
        }

        private void SortTable(string columnName)
        {
            //Sorting against a column that is not currently sorted against.
            if (columnName != CurrentSortColumn)
            {
                //We need to force order by ascending on the new column
                //This line uses reflection and will probably 
                //perform inefficiently in a production environment.
                ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplay.OrderBy(x =>
                                        x.GetType()
                                        .GetProperty(columnName)
                                        .GetValue(x, null))
                              .ToList();
                CurrentSortColumn = columnName;
                IsSortedAscending = true;

            }
            else //Sorting against same column but in different direction
            {
                if (IsSortedAscending)
                {
                    ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplay.OrderByDescending(x =>
                                                      x.GetType()
                                                       .GetProperty(columnName)
                                                       .GetValue(x, null))
                                 .ToList();
                }
                else
                {
                    ReservoirsLinkedToUserForDisplay = ReservoirsLinkedToUserForDisplay.OrderBy(x =>
                                            x.GetType()
                                             .GetProperty(columnName)
                                             .GetValue(x, null))
                                             .ToList();
                }

                //Toggle this boolean
                IsSortedAscending = !IsSortedAscending;
            }
        }

    private void Dispose()
        {
            this.Dispose(true);
        }

        public string text1 = "";
        public string text2 = "";

        public bool IsEnabled = false;

        public async Task OnTabChanged(Tab tab)
        {
            text1 = $"Tab value: {tab.Value}";
            text2 = $"Tab text: {tab.Text}";
        }        
    }
}
