using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationWPF
{
    public static class ValidationHelpers
    {
        public static bool IsValidUserName(this string userName)
        {
            bool result = false;

            if (userName.Length <= 50 && userName.Length >0)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            bool result = false;

            if (password.Length >= 7)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidEmail(this string email)
        {
            var result = false;

            if (email.Length > 6
                && email.Length <= 100
                && email.Contains("@")
                && email.Contains("."))
            {
                result = true;
            }

            return result;
        }
        public static bool isValidFirstName(this string firstName)
        {
            bool result = false;

            if (firstName.Length >= 1 && firstName.Length <= 50)
            {
                result = true;
            }

            return result;
        }
        public static bool isValidLastName(this string lastName)
        {
            bool result = false;

            if (lastName.Length >= 1 && lastName.Length <= 100)
            {
                result = true;
            }

            return result;
        }
    }
}
