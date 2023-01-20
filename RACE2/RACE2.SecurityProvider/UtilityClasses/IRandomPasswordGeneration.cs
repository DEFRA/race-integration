using Microsoft.AspNetCore.Identity;

namespace RACE2.SecurityProvider.UtilityClasses
{
    public interface IRandomPasswordGeneration
    {
        public string GenerateRandomPassword(PasswordOptions opts);
    }
}
