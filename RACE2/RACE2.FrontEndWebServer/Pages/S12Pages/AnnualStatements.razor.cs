using DocumentFormat.OpenXml.InkML;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using MudBlazor;
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
using static MudBlazor.CategoryTypes;

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
        private IEnumerable<Claim> Claims { get; set; }
        private List<UndertakerDTO> Undertakers { get; set; }

        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();
        CultureInfo en = CultureInfo.GetCultureInfo("en-US");
        int selectedReservoirId= 0;
        protected async override Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                PhoneNumber = userDetails.PhoneNumber,
                Addresses = userDetails.Addresses,
                c_first_name = userDetails.c_first_name,
                c_last_name = userDetails.c_last_name,
                c_IsFirstTimeUser = userDetails.c_IsFirstTimeUser
            };
            if (UserDetail.c_IsFirstTimeUser)
            {
                bool forceLoad = true;
                Uri pagelink = new Uri(_config["RACE2SecurityProviderURL"] + "/Identity/Account/ChangeYourPassword");
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
                var undertakers = await reservoirService.GetOperatorsforReservoir(reservoir.Id, reservoir.OperatorType);
                ReservoirsLinkedToUserForDisplay reservoirsLinkedToUser=new ReservoirsLinkedToUserForDisplay();
                reservoirsLinkedToUser.ReservoirName = reservoir.PublicName;
                if (undertakers != null && undertakers.Count() > 0)
                {
                    if (!String.IsNullOrEmpty(undertakers[0].OperatorFirstName))
                        reservoirsLinkedToUser.UndertakerName = undertakers[0].OperatorFirstName + " " + undertakers[0].OperatorLastName;
                    else if (!String.IsNullOrEmpty(undertakers[0].OrgName))
                        reservoirsLinkedToUser.UndertakerName = undertakers[0].OrgName;
                    else
                        reservoirsLinkedToUser.UndertakerName = "";
                }
                var submissionStatusList = await reservoirService.GetReservoirStatusByUserId(UserDetail.Id);
                var submisstionStatus = submissionStatusList.Where(s => s.PublicName == reservoir.PublicName).FirstOrDefault();
                reservoirsLinkedToUser.DueDate = submisstionStatus.DueDate!=DateTime.MinValue? submisstionStatus.DueDate.ToString("dd MMMMM yyyy") :"";
                reservoirsLinkedToUser.Status = submisstionStatus.Status!=null? submisstionStatus.Status:"Not Started";
                ReservoirsLinkedToUserForDisplay.Add(reservoirsLinkedToUser);
            }
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        protected override async void OnAfterRender(bool firstRender)
        {

        }

        // custom sort by name length
        private Func<ReservoirsLinkedToUserForDisplay, object> _sortBy => x =>
        {
            if (_sortNameByLength)
                return x.ReservoirName.Length;
            else
                return x.ReservoirName;
        };

        // quick filter - filter globally across multiple columns with the same input
        private Func<ReservoirsLinkedToUserForDisplay, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.ReservoirName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        void RowClicked(DataGridRowClickEventArgs<ReservoirsLinkedToUserForDisplay> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
            //DownloadReportTemplate(args.Item);
        }

        void SelectedItemsChanged(HashSet<ReservoirsLinkedToUserForDisplay> items)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        }
        void SelectedItemChanged(ReservoirsLinkedToUserForDisplay item)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");

        }

        private async void DownloadReportTemplate(ReservoirsLinkedToUserForDisplay item)
        {
            var reservoir= ReservoirsLinkedToUser.Where(r=>r.PublicName==item.ReservoirName).FirstOrDefault();
            var undertaker=Undertakers.Where(u=>u.ReservoirId==reservoir.Id).FirstOrDefault();
            var blobName = "S12ReportTemplate.docx";
            Stream response = await blobStorageService.GetBlobFileStream(blobName);
            S12PrePopulationFields s12PrePopulationFields = new S12PrePopulationFields();
            s12PrePopulationFields.ReservoirName = reservoir.PublicName;
            s12PrePopulationFields.ReservoirNearestTown = reservoir.NearestTown != null ? reservoir.NearestTown : "";
            s12PrePopulationFields.ReservoirGridRef = reservoir.GridReference != null ? reservoir.GridReference : "";
            s12PrePopulationFields.SupervisingEngineerName = UserDetail.c_first_name + " " + UserDetail.c_last_name;
            //s12PrePopulationFields.SupervisingEngineerAddress = UserDetail.Addresses[0].Address.AddressLine1;
            s12PrePopulationFields.SupervisingEngineerEmail = UserDetail.Email;
            s12PrePopulationFields.SupervisingEngineerPhoneNumber = UserDetail.PhoneNumber != null ? UserDetail.PhoneNumber : "";

            MemoryStream processedStream = openXMLUtilitiesService.SearchAndReplace(response, s12PrePopulationFields);
            processedStream.Position = 0;
            var streamRef = new DotNetStreamReference(stream: processedStream);
            await jsRuntime.InvokeVoidAsync("downloadFileFromStream", blobName, streamRef);
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
