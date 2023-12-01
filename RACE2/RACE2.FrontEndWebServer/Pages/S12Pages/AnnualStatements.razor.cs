using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IOpenXMLUtilitiesService openXMLUtilitiesService { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IReservoirService reservoirService { get; set; } = default!;
        bool IsAdmin=false;
        bool IsFirstTimeUser=false;
        private string UserName { get; set; } = "Unknown";
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();
        private List<ReservoirDetailsDTO> ReservoirDetailsLinkedToUser { get; set; } = new List<ReservoirDetailsDTO>();
        private List<ReservoirsLinkedToUserForDisplay> ReservoirsLinkedToUserForDisplay { get; set; } = new List<ReservoirsLinkedToUserForDisplay>();
        private List<ReservoirsLinkedToUserForDisplay> ReservoirsLinkedToUserForDisplayOnStart { get; set; } = new List<ReservoirsLinkedToUserForDisplay>();
        private IEnumerable<Claim> Claims { get; set; }
        private List<OperatorDTO> Undertakers { get; set; }
        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();
        CultureInfo en = CultureInfo.GetCultureInfo("en-US");
        int selectedReservoirId = 0;

        //We need a field to tell us which direction the table is currently sorted by
        private bool IsSortedAscending;

        //We also need a field to tell us which column the table is sorted by.
        private string CurrentSortColumn;
        UserSpecificDto userDetails { get; set; }
        UserSpecificDto userDetailsWithRoles { get; set; }
        List<SubmissionStatusDTO> SubmissionStatusList { get; set; }
        SubmissionStatusDTO SubmissionStatus { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthenticationStateTask;
                UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
                userDetails = await userService.GetUserByEmailID(UserName);
                //userDetailsWithRoles = await userService.GetUserWithRoles(UserName);
                foreach (var role in userDetails.roles)
                {
                    if (role.Name == "System Administrator")
                    {
                        IsAdmin = true;
                        break;
                    }
                }
                if (IsAdmin)
                {
                    NavigationManager.NavigateTo("/race2-admin", true);
                }

                if ((bool)userDetails.cIsFirstTimeUser)
                {
                    IsFirstTimeUser = true;
                    bool forceLoad = true;
                    string pagelink = _config["RACE2SecurityProviderURL"] + "/Identity/Account/CreatePassword?userEmail=" + UserName;
                    NavigationManager.NavigateTo(pagelink, forceLoad);
                }

                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application","FrontEndWebServer").ForContext("Method","AnnualStatement").Information(UserName + " accessed S12 template generation.");

                ReservoirDetailsLinkedToUser = await reservoirService.GetReservoirsByUserId(userDetails.Id);

                foreach (var rn in ReservoirDetailsLinkedToUser)
                {
                    try
                    {
                        var r = new Reservoir()
                        {
                            Id = rn.Id,
                            RaceReservoirId = rn.RaceReservoirId,
                            PublicName = rn.PublicName,
                            RegisteredName = rn.RegisteredName,
                            NearestTown = rn.NearestTown,
                            GridReference = rn.GridReference,
                            OperatorType = rn.OperatorType,
                            LastInspectionByUser = rn.UserDetail,
                            NextInspectionDate102 = rn.NextInspectionDate102,
                            NextInspectionDate103 = rn.NextInspectionDate103,
                            LastInspectionDate = rn.LastInspectionDate,
                            LastCertificationDate = rn.LastCertificationDate

                        };
                        //r.Address = new Address()
                        //{
                        //    AddressLine1 = rn.Address.AddressLine1,
                        //    AddressLine2 = rn.Address.AddressLine2,
                        //    Town = rn.Address.Town,
                        //    County = rn.Address.County,
                        //    Postcode = rn.Address.Postcode
                        //};
                        ReservoirsLinkedToUser.Add(r);
                    }
                    catch (Exception ex)
                    {
                        Serilog.Log.Logger.Fatal("On init for Reservoir :"+ rn.Id.ToString()+" Error getting data from backend services : " + ex.Message);
                    }
                }
                PopulateReservoirsToDisplay(ReservoirsLinkedToUser);
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });                
            }
            catch (Exception ex)
            {
                //Log.Logger.Fatal("Error getting data from backend services : "+ex.Message);
                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "AnnualStatement OnInitializedAsync").Fatal("Error getting data from backend services : " + ex.Message);
                //throw new ApplicationException("Error loading annual statement data.");
                bool forceLoad = false;
                string pagelink = "/ApplicationError";
                NavigationManager.NavigateTo(pagelink, forceLoad);
            }
            finally
            {
                await base.OnInitializedAsync();
            };
        }

        private async void PopulateReservoirsToDisplay(List<Reservoir> reservoirs)
        {
            try
            {
                foreach (var reservoir in reservoirs)
                {
                    try
                    {
                        Undertakers = await reservoirService.GetOperatorsforReservoir(reservoir.Id, reservoir.OperatorType);
                        ReservoirsLinkedToUserForDisplay reservoirsLinkedToUser = new ReservoirsLinkedToUserForDisplay();
                        reservoirsLinkedToUser.ReservoirID = reservoir.Id;
                        reservoirsLinkedToUser.ReservoirName = reservoir.RegisteredName;
                        if (Undertakers != null && Undertakers.Count() > 0)
                        {
                            if (!String.IsNullOrEmpty(Undertakers[0].OrgName))
                                reservoirsLinkedToUser.UndertakerName = Undertakers[0].OrgName;
                            else if (!String.IsNullOrEmpty(Undertakers[0].OperatorFirstName))
                                reservoirsLinkedToUser.UndertakerName = Undertakers[0].OperatorFirstName + " " + Undertakers[0].OperatorLastName;
                            else
                                reservoirsLinkedToUser.UndertakerName = "";
                            if (!String.IsNullOrEmpty(Undertakers[0].Email))
                            {
                                reservoirsLinkedToUser.UndertakerEmail = Undertakers[0].Email;
                            }
                            else
                            {
                                reservoirsLinkedToUser.UndertakerEmail = "";
                            }
    }
                        SubmissionStatusList = await reservoirService.GetReservoirStatusByUserId(userDetails.Id);
                        SubmissionStatus = SubmissionStatusList.Where(s => s.RegisteredName == reservoir.RegisteredName).FirstOrDefault();
                        reservoirsLinkedToUser.SubmissionReference = SubmissionStatus.SubmissionReference;
                        reservoirsLinkedToUser.DueDate = SubmissionStatus.DueDate != DateTime.MinValue ? SubmissionStatus.DueDate.ToString("dd MMMMM yyyy") : String.Empty;
                        reservoirsLinkedToUser.Status = SubmissionStatus.Status != null ? SubmissionStatus.Status : String.Empty;

                        ReservoirsLinkedToUserForDisplay.Add(reservoirsLinkedToUser);
                    }
                    catch (Exception ex)
                    {
                        Serilog.Log.Logger.Fatal("On PopulateReservoirsToDisplay for Reservoir :" + reservoir.Id.ToString() + " Error getting data from backend services : " + ex.Message);
                    }
                }
                ReservoirsLinkedToUserForDisplayOnStart = ReservoirsLinkedToUserForDisplay;
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            catch (Exception ex)
            {
                //Log.Logger.Fatal("Error loading reservoir data : " + ex.Message);
                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "AnnualStatement PopulateReservoirsToDisplay").Fatal("Error loading reservoir data : " + ex.Message);
                //throw new ApplicationException("Error loading reservoir data.");
                bool forceLoad = false;
                string pagelink = "/ApplicationError";
                NavigationManager.NavigateTo(pagelink, forceLoad);
            };
        }

        private async void DownloadReportTemplate(ReservoirsLinkedToUserForDisplay item)
        {
            try
            {
                var reservoir = ReservoirsLinkedToUser.Where(r => r.RegisteredName == item.ReservoirName).FirstOrDefault();
                SubmissionStatus = SubmissionStatusList.Where(s => s.RegisteredName == reservoir.RegisteredName).FirstOrDefault();
                var Undertakers = await reservoirService.GetOperatorsforReservoir(reservoir.Id, reservoir.OperatorType);

                SubmissionStatus updatedStatus = await reservoirService.UpdateReservoirStatus(reservoir.Id, userDetails.Id, "In progress");

                var blobName = updatedStatus.OverrideUsedTemplate + ".docx";
                //var blobName = "S12ReportTemplate.docx";
                //var blobName = "TestWithTags.docx";
                Stream response = await blobStorageService.GetBlobFileStream(blobName);
                S12PrePopulationFields s12PrePopulationFields = new S12PrePopulationFields();
                s12PrePopulationFields.ReservoirName = reservoir.RegisteredName;
                s12PrePopulationFields.ReservoirNearestTown = reservoir.NearestTown != null ? reservoir.NearestTown : "";
                s12PrePopulationFields.ReservoirGridRef = reservoir.GridReference != null ? reservoir.GridReference : "";
                s12PrePopulationFields.SupervisingEngineerName = userDetails.cFirstName + " " + userDetails.cLastName;
                s12PrePopulationFields.SupervisingEngineerCompanyName = " ";
                Address address = userDetails.addresses.FirstOrDefault();
                s12PrePopulationFields.SupervisingEngineerAddress = address.AddressLine1;
                if (!String.IsNullOrEmpty(address.AddressLine2))
                    s12PrePopulationFields.SupervisingEngineerAddress = s12PrePopulationFields.SupervisingEngineerAddress + ", " + address.AddressLine2;
                if (!String.IsNullOrEmpty(address.Town))
                    s12PrePopulationFields.SupervisingEngineerAddress = s12PrePopulationFields.SupervisingEngineerAddress + ", " + address.Town;
                if (!String.IsNullOrEmpty(address.County))
                    s12PrePopulationFields.SupervisingEngineerAddress = s12PrePopulationFields.SupervisingEngineerAddress + ", " + address.County;
                if (!String.IsNullOrEmpty(address.Postcode))
                    s12PrePopulationFields.SupervisingEngineerAddress = s12PrePopulationFields.SupervisingEngineerAddress + ", " + address.Postcode;
                s12PrePopulationFields.SupervisingEngineerEmail = userDetails.Email;
                if (!String.IsNullOrEmpty(userDetails.cMobile))
                    s12PrePopulationFields.SupervisingEngineerPhoneNumber = userDetails.cMobile;
                else if (!String.IsNullOrEmpty(userDetails.cAlternativeMobile))
                    s12PrePopulationFields.SupervisingEngineerPhoneNumber = userDetails.cAlternativeMobile;
                else if (!String.IsNullOrEmpty(userDetails.cAlternativePhone))
                    s12PrePopulationFields.SupervisingEngineerPhoneNumber = userDetails.cAlternativePhone;
                else if (!String.IsNullOrEmpty(userDetails.cAlternativeEmergencyPhone))
                    s12PrePopulationFields.SupervisingEngineerPhoneNumber = userDetails.cAlternativeEmergencyPhone;
                else
                    s12PrePopulationFields.SupervisingEngineerPhoneNumber = userDetails.PhoneNumber != null ? userDetails.PhoneNumber : "";
                Undertakers = await reservoirService.GetOperatorsforReservoir(reservoir.Id, reservoir.OperatorType);
                s12PrePopulationFields.UndertakerName = item.UndertakerName;
                s12PrePopulationFields.UndertakerEmail = Undertakers[0].Email;
                s12PrePopulationFields.UndertakerAddress = Undertakers[0].AddressLine1;
                if (!String.IsNullOrEmpty(Undertakers[0].AddressLine2))
                    s12PrePopulationFields.UndertakerAddress = s12PrePopulationFields.UndertakerAddress + ", " + Undertakers[0].AddressLine2;
                if (!String.IsNullOrEmpty(Undertakers[0].Town))
                    s12PrePopulationFields.UndertakerAddress = s12PrePopulationFields.UndertakerAddress + ", " + Undertakers[0].Town;
                if (!String.IsNullOrEmpty(Undertakers[0].County))
                    s12PrePopulationFields.UndertakerAddress = s12PrePopulationFields.UndertakerAddress + ", " + Undertakers[0].County;
                if (!String.IsNullOrEmpty(Undertakers[0].Postcode))
                    s12PrePopulationFields.UndertakerAddress = s12PrePopulationFields.UndertakerAddress + ",  " + Undertakers[0].Postcode;
                if (!String.IsNullOrEmpty(Undertakers[0].cMobile))
                    s12PrePopulationFields.UndertakerPhoneNumber = Undertakers[0].cMobile;
                else if (!String.IsNullOrEmpty(Undertakers[0].cAlternativeMobile))
                    s12PrePopulationFields.UndertakerPhoneNumber = Undertakers[0].cAlternativeMobile;
                else if (!String.IsNullOrEmpty(Undertakers[0].cAlternativePhone))
                    s12PrePopulationFields.UndertakerPhoneNumber = Undertakers[0].cAlternativePhone;
                else if (!String.IsNullOrEmpty(Undertakers[0].cAlternativeEmergencyPhone))
                    s12PrePopulationFields.UndertakerPhoneNumber = Undertakers[0].cAlternativeEmergencyPhone;
                else 
                    s12PrePopulationFields.UndertakerPhoneNumber = "";
                if (reservoir.NextInspectionDate103 != DateTime.MinValue)
                {
                    s12PrePopulationFields.NextInspectionDate = (reservoir.NextInspectionDate103 != DateTime.MinValue) && (reservoir.LastCertificationDate.HasValue) ? reservoir.NextInspectionDate103.Value.ToString("dd MMMM yyyy") : " ";
                }
                else
                {
                    s12PrePopulationFields.NextInspectionDate = (reservoir.NextInspectionDate102 != DateTime.MinValue) && (reservoir.LastCertificationDate.HasValue) ? reservoir.NextInspectionDate102.Value.ToString("dd MMMM yyyy") : " ";
                }
                s12PrePopulationFields.LastCertificationDate = (reservoir.LastCertificationDate != DateTime.MinValue) && (reservoir.LastCertificationDate.HasValue) ? reservoir.LastCertificationDate.Value.ToString("dd MMMM yyyy"): " ";
                s12PrePopulationFields.LastInspectionDate = (reservoir.LastInspectionDate != DateTime.MinValue) && (reservoir.LastInspectionDate.HasValue) ? reservoir.LastInspectionDate.Value.ToString("dd MMMM yyyy"): " ";
                if ((reservoir.LastInspectionByUser.Id != 0) && (reservoir.LastInspectionByUser != null))
                {
                    s12PrePopulationFields.LastInspectingEngineerName = !String.IsNullOrEmpty(reservoir.LastInspectionByUser.cFirstName) && !String.IsNullOrEmpty(reservoir.LastInspectionByUser.cLastName) ? reservoir.LastInspectionByUser.cFirstName + " " + reservoir.LastInspectionByUser.cLastName : " ";
                    if (!String.IsNullOrEmpty(reservoir.LastInspectionByUser.cMobile))
                        s12PrePopulationFields.LastInspectingEngineerPhoneNumber = reservoir.LastInspectionByUser.cMobile;
                    else if (!String.IsNullOrEmpty(reservoir.LastInspectionByUser.cAlternativeMobile))
                        s12PrePopulationFields.LastInspectingEngineerPhoneNumber = reservoir.LastInspectionByUser.cAlternativeMobile;
                    else if (!String.IsNullOrEmpty(reservoir.LastInspectionByUser.cAlternativePhone))
                        s12PrePopulationFields.LastInspectingEngineerPhoneNumber = reservoir.LastInspectionByUser.cAlternativePhone;
                    else if (!String.IsNullOrEmpty(reservoir.LastInspectionByUser.cAlternativeEmergencyPhone))
                        s12PrePopulationFields.LastInspectingEngineerPhoneNumber = reservoir.LastInspectionByUser.cAlternativeEmergencyPhone;
                    else
                        s12PrePopulationFields.LastInspectingEngineerPhoneNumber = "";
                }
                else
                {
                    s12PrePopulationFields.LastInspectingEngineerName = !String.IsNullOrEmpty(reservoir.LastInspectionEngineerName) ? reservoir.LastInspectionEngineerName : " ";
                    s12PrePopulationFields.LastInspectingEngineerPhoneNumber = !String.IsNullOrEmpty(reservoir.LastInspectionEngineerPhone) ? reservoir.LastInspectionEngineerPhone : " ";
                }
                MemoryStream processedStream = openXMLUtilitiesService.SearchAndReplace(response, s12PrePopulationFields);
                processedStream.Position = 0;
                var streamRef = new DotNetStreamReference(stream: processedStream);
                string downloadedFileName = reservoir.RegisteredName + " S12.docx";
                await jsRuntime.InvokeVoidAsync("downloadFileFromStream", downloadedFileName, streamRef);
                var reservoirLinkedToUser = ReservoirsLinkedToUserForDisplay.Where(r => r.ReservoirName == reservoir.RegisteredName).FirstOrDefault();
                reservoirLinkedToUser.Status = updatedStatus.Status;
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });          
            }    
            catch (Exception ex)
            {
                //Log.Logger.Fatal("Error downloading S12ReportTemplate for the reservoir : " + ex.Message);
                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "AnnualStatement DownloadReportTemplate").Fatal("Error downloading S12ReportTemplate for the reservoir : " + ex.Message);
                //throw new ApplicationException("Error downloading S12ReportTemplate for the reservoir.");
                bool forceLoad = false;
                string pagelink = "/ApplicationError";
                NavigationManager.NavigateTo(pagelink, forceLoad);
            };
        }

        private void DownloadTemplateFile(ReservoirsLinkedToUserForDisplay Item)
        {
            DownloadReportTemplate(Item);
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
            var reservoir = ReservoirsLinkedToUser.Where(s => s.RegisteredName == reservoirStatus.RegisteredName).FirstOrDefault();
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            if (reservoirStatus.Status.ToUpper() == "DRAFT SENT")
            {
                pagelink = "/s12-statement-confirmation-draft-sent";
            }
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void gotoSubmissionPage(ReservoirsLinkedToUserForDisplay Item)
        {
            bool forceLoad = false;
            string pagelink = $"/send-your-statement/{Item.ReservoirID}/{Item.ReservoirName}/{Item.UndertakerName}/{Item.UndertakerEmail}/{Item.SubmissionReference}";
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
    }
}
