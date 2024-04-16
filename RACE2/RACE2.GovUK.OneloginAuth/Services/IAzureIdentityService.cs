using System.Threading.Tasks;

namespace RACE2.GovUK.OneloginAuth.Services
{
    public interface IAzureIdentityService
    {
        Task<string> AuthenticationCallback(string authority, string resource, string scope);
    }
}