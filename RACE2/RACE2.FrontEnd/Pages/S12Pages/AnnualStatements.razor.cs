using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEnd.Components;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class AnnualStatements
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        string CurrentUserEmail;
        bool? IsLoggedIn;
        private IEnumerable<Claim> UserClaims { get; set; }
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } 
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUser { get; set; } = new List<SubmissionStatusDTO>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUserComplete { get; set; } = new List<SubmissionStatusDTO>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUserDraft { get; set; } = new List<SubmissionStatusDTO>();

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.Name is not null)
            {
                UserName = authState.User.Identity.Name;
                UserClaims = authState.User.Claims;
            }
            //var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            //UserId = userDetails!.Data!.UserByEmailID.Id;
            var userDetails = await client.GetUserWithRoles.ExecuteAsync(UserName);
            UserId = userDetails!.Data!.UserWithRoles.Id;
            UserDetail = new UserDetail()
            {
                UserName= UserName,
                Id= UserId,
                //Email= userDetails!.Data!.UserByEmailID.Email
                Email = userDetails!.Data!.UserWithRoles.Email
            };

            var resultsOfReservoirWithStatus = await client.GetReservoirStatusByEmail.ExecuteAsync(UserDetail.Email);

            var reservoirStatusLinkedToUser = resultsOfReservoirWithStatus!.Data!.ReservoirStatusByEmail;
            foreach (var rs in reservoirStatusLinkedToUser)
            {
                var s = new SubmissionStatusDTO()
                {
                    PublicName = rs.PublicName,
                    SubmittedOn= new DateTime(rs.SubmittedOn.Year, rs.SubmittedOn.Month, rs.SubmittedOn.Day),
                    Status = rs.Status
                };
                ReservoirStatusLinkedToUser.Add(s);
            }
            ReservoirStatusLinkedToUserComplete= ReservoirStatusLinkedToUser.Where(st=>st.Status.ToUpper() == "COMPLETE").ToList();
            ReservoirStatusLinkedToUserDraft = ReservoirStatusLinkedToUser.Where(st => st.Status.ToUpper() != "COMPLETE").ToList();
            var results = await client.GetReservoirsByUserId.ExecuteAsync(UserId);

            var reservoirs = results!.Data!.ReservoirsByUserId;

            foreach (var rn in reservoirs)
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

            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitialized();
        }

        public async void GoToNextPage()
        {
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
            var reservoir= ReservoirsLinkedToUser.Where(s=>s.PublicName== reservoirStatus.PublicName).FirstOrDefault();
            var action = new StoreReservoirAction(reservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            if (reservoirStatus.Status.ToUpper()=="DRAFT SENT")
            {
                pagelink= "/s12-statement-confirmation-draft-sent";
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

        //public string text1 = "";
        //public string text2 = "";

        //public bool IsEnabled = false;

        //public async Task OnTabChanged(Tab tab)
        //{
        //    text1 = $"Tab value: {tab.Value}";
        //    text2 = $"Tab text: {tab.Text}";
        //}
        //
        private bool submitted = true;
        private bool drafts = false;
        private void DisplayTab(int TabNumber)
        {
            switch (TabNumber)
            {
                case 1:
                    this.submitted = true;
                    this.drafts = false;
                    break;
                case 2:
                    this.submitted = false;
                    this.drafts = true;
                    break;
            }
        }
    }
}
