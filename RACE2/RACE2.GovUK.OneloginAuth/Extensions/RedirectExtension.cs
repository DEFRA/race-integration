namespace RACE2.GovUK.OneloginAuth.Extensions
{
    public static class RedirectExtension
    {
        public static string GetEnvironmentAndDomain(this string redirectUri, string environment)
        {
            if (environment.ToLower() == "development")
            {
                return "";
            }
            if (!string.IsNullOrEmpty(redirectUri))
            {
                return redirectUri;
            }
            var environmentPart = environment.ToLower() == "production" ? "manage-apprenticeships" : $"{environment.ToLower()}-eas.apprenticeships";
            var domainPart = environment.ToLower() == "production" ?  "service" : "education";

            return $"{environmentPart}.{domainPart}.gov.uk";
        }
        
        public static string GetSignedOutRedirectUrl(this string redirectUri, string environment)
        {
            if (!string.IsNullOrEmpty(redirectUri))
            {
                return redirectUri;
            }

            //return $"https://employerprofiles.{"".GetEnvironmentAndDomain(environment)}/service/user-signed-out";
            return "https://oidc.integration.account.gov.uk/logout";
        }

        public static string GetAccountSuspendedRedirectUrl(string environment)
        {   
            return $"https://employerprofiles.{"".GetEnvironmentAndDomain(environment)}/service/account-unavailable";
        }
    
        public static string GetStubSignInRedirectUrl(this string redirectUrl, string environment)
        {
            if (environment.ToLower() == "development" || environment.ToLower() == "production")
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return redirectUrl;
            }
            
            return $"https://employerprofiles.{environment.ToLower()}-eas.apprenticeships.education.gov.uk/service/account-details";
        }
    }    
}

