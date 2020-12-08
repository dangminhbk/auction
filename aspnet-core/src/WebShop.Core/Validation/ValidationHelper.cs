using Abp.Extensions;
using System.Text.RegularExpressions;

namespace WebShop.Validation
{
    public static class ValidationHelper
    {
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static bool IsEmail(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            Regex regex = new Regex(EmailRegex);
            return regex.IsMatch(value);
        }
    }
}
