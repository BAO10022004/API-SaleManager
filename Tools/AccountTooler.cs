using System.Text.RegularExpressions;

namespace SaleManagerWebAPI.Tools
{
    public class AccountTooler
    {
        private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        RegexOptions.Compiled
    );
        public static bool IsEmail(string input)
        {
            
            string trimmed = input.Trim();

            if (trimmed.Contains("@"))
            {
                if (IsValidEmail(trimmed))
                    return true;
                else return false;
            }
            return false;

        }
        private static bool IsValidEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
                return false;

            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            string localPart = parts[0];
            string domainPart = parts[1];

            return localPart.Length <= 64 &&
                   domainPart.Length <= 253 &&
                   domainPart.Contains(".") &&
                   !localPart.StartsWith(".") && !localPart.EndsWith(".") &&
                   !domainPart.StartsWith(".") && !domainPart.EndsWith(".") &&
                   !email.Contains("..");
        }

    }


}
