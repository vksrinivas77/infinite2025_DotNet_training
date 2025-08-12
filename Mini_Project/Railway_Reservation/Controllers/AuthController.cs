using System;
using RailwayReservation.Services;

namespace RailwayReservation.Controllers
{
    public class AuthController
    {
        private readonly AuthService _auth;
        public int LoggedInUserId { get; private set; }
        public string LoggedInUserName { get; private set; }

        public AuthController(AuthService auth) { _auth = auth; }

        public bool LoginFlow(string role)
        {
            Console.Clear();
            Console.WriteLine($"=== {role.ToUpper()} LOGIN ===");
            Console.WriteLine("Enter 0 to go back.\n");

            string username = PromptNonEmptyAllowBack("Username: ");
            if (username == null) return false;
            string password = PromptNonEmptyAllowBack("Password: ");
            if (password == null) return false;

            if (_auth.ValidateLogin(username, password, role, out int uid, out string fullname))
            {
                LoggedInUserId = uid;
                LoggedInUserName = fullname;
                Console.WriteLine($"\nWelcome, {fullname}! Press Enter.");
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("\nInvalid credentials or inactive. Press Enter.");
            Console.ReadLine();
            return false;
        }

        public void RegisterFlow()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER ===");
            Console.WriteLine("Enter 0 to go back.\n");

            string fullname = PromptNonEmptyAllowBack("Full name: ");
            if (fullname == null) return;
            string email = PromptNonEmptyAllowBack("Email: ");
            if (email == null) return;
            string phone = PromptNonEmptyAllowBack("Phone: ");
            if (phone == null) return;
            string username = PromptNonEmptyAllowBack("Username: ");
            if (username == null) return;
            string password = PromptNonEmptyAllowBack("Password: ");
            if (password == null) return;

            bool ok = _auth.RegisterUser(username, password, fullname, email, phone, out string msg);
            Console.WriteLine(msg);
            Console.WriteLine("Press Enter.");
            Console.ReadLine();
        }

        private string PromptNonEmptyAllowBack(string label)
        {
            while (true)
            {
                Console.Write(label);
                var input = Console.ReadLine();
                if (input == "0") return null;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This field cannot be empty. Try again or enter 0 to go back.");
                    continue;
                }
                return input.Trim();
            }
        }
    }
}
