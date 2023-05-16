using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;
using RACE2.FrontEndWeb.Utilities;
using System;

namespace RACE2.FrontEndWeb.Components
{
    public partial class EnterEmail
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        string? Email;
        private UserDetail CurrentUser;
        private UserModel userModel => new UserModel();

        private string errorMsg1 = "Email entered does not exist!!! Please try again.";
        private string errorMsg2 = "Input is not a valid email address!!! Please try again.";

        protected override void OnInitialized()
        {        

        }

        protected async Task<string> GetEmailFromDatabase(string email)
        {
            var result = await client.GetUserByEmailID.ExecuteAsync(email);
            var result1 = await client.GetReservoirsByUserId.ExecuteAsync(result.Data.UserByEmailID.Id);
            if (result != null && result.Data != null && result.Data.UserByEmailID.Email != null)
            {
                string pwh = result!.Data!.UserByEmailID.PasswordHash;
                CurrentUser = new UserDetail()
                {
                    Id = result!.Data!.UserByEmailID.Id,
                    Email = result!.Data!.UserByEmailID.Email,
                    UserName = result!.Data!.UserByEmailID.UserName,
                    PasswordHash = result!.Data!.UserByEmailID.PasswordHash
                };
                return result!.Data!.UserByEmailID.Email;
            }
            else
                return "";
        }

        public async Task GoToNextPage()
        {
            string email = userModel.Email;
            string password = userModel.Password;
            if (email != null && email.Trim().Length != 0 && RegexUtilities.IsValidEmail(email))
            {
                bool forceLoad = false;

                string emailInDatabase = await GetEmailFromDatabase(email);
                if (emailInDatabase != null && emailInDatabase.Trim().Length != 0)
                {
                    if (CurrentUser.PasswordHash is null)
                    {
                        string pagelink = "/enter-password";
                        NavigationManager.NavigateTo(pagelink, forceLoad);
                    }
                    else
                    {
                        string pagelink = "/";
                        NavigationManager.NavigateTo(pagelink, forceLoad);
                    }
                }
                else
                {
                    Email = errorMsg1;                   
                }
            }
            else
            {
                Email = errorMsg2;                
            }
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }

    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
