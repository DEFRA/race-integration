using System.Security.Permissions;

namespace RACE2.SecurityProvider.UtilityClasses
{
    public interface IPasswordValidator
    {
        bool ValidatePassword(string password);
    }
}
