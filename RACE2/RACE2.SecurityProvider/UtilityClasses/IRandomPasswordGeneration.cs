using Microsoft.AspNetCore.Identity;

namespace RACE2.SecurityProvider.UtilityClasses
{
    public interface IRandomPasswordGeneration
    {
        string GenerateRandomPassword(PasswordOptions opts);
    }
}
