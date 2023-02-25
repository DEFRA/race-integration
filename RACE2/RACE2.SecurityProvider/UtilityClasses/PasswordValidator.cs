using System.Text.RegularExpressions;

namespace RACE2.SecurityProvider.UtilityClasses
{
    public class PasswordValidator : IPasswordValidator
    {
        public bool ValidatePassword(string password)
        {
            string regex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&amp;])[A-Za-z\d$@$!%*#?&amp;]{8,}$";
            var match = Regex.Match(password, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
