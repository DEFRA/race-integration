namespace RACE2.GovUK.OneloginAuth.Configuration
{
    public class GovUkOidcConfiguration
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string KeyVaultIdentifier { get; set; }
        public int LoginSlidingExpiryTimeOutInMinutes { get; set; } = 30;
        public string GovLoginSessionConnectionString { get; set; }
    }

    public static class GovUkConstants
    {
        public const string StubAuthCookieName = "Race2App.StubAuthCookie";
        public const string AuthCookieName = "Race2App.AuthCookie";
    }
}