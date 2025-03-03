using System.Text.RegularExpressions;

namespace BussinesLayer
{
    public class Utils
    {
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePhone(string phone)
        {
            string phonePattern = @"^[0-9]{10}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        public static bool ValidateName(string name)
        {
            string namePattern = @"^[a-zA-Z\s]{2,}$";
            return Regex.IsMatch(name, namePattern);
        }

        public static bool validatePosition(string position){
            if (position == "root" || position == "admin" || position == "employee") return true;
            return false;
        }
    }
}
