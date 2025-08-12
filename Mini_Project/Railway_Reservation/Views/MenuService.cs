using System;
using RailwayReservation.Controllers;

namespace RailwayReservation.Views
{
    public static class MenuService
    {
        public static void ShowMainMenu(AuthController auth, AdminController admin, UserController user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== RAILWAY RESERVATION =====");
                Console.WriteLine("1. User Login");
                Console.WriteLine("2. Admin Login");
                Console.WriteLine("3. Register");
                Console.WriteLine("4. Exit");
                Console.Write("Choice: ");
                var ch = Console.ReadLine();
                if (ch == "1")
                {
                    if (auth.LoginFlow("user"))
                    {
                        UserMenu(user, auth);
                    }
                }
                else if (ch == "2")
                {
                    if (auth.LoginFlow("admin"))
                    {
                        AdminMenu(admin);
                    }
                }
                else if (ch == "3")
                {
                    auth.RegisterFlow();
                }
                else if (ch == "4")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid. Press Enter.");
                    Console.ReadLine();
                }
            }
        }

        private static void AdminMenu(AdminController admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ADMIN MENU ===");
                Console.WriteLine("1. Add Train");
                Console.WriteLine("2. Modify Train");
                Console.WriteLine("3. Delete Train");
                Console.WriteLine("4. Manage Stations");
                Console.WriteLine("5. All Bookings");
                Console.WriteLine("6. All Users");
                Console.WriteLine("7. All Cancellations");
                Console.WriteLine("8. Showalltrains");
                Console.WriteLine("0. Logout / Back");
                Console.Write("Choice: ");
                var ch = Console.ReadLine();
                switch (ch)
                {
                    case "1": admin.AddTrainFlow(); break;
                    case "2": admin.ModifyTrainFlow(); break;
                    case "3": admin.DeleteTrainFlow(); break;
                    case "4": admin.ManageStationsFlow(); break;
                    case "5": admin.ShowAllBookings(); break;
                    case "6": admin.ShowAllUsers(); break;
                    case "7": admin.ShowAllCancellations(); break;
                    case "8": admin.ShowAllTrains(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid. Press Enter."); Console.ReadLine(); break;
                }
            }
        }

        private static void UserMenu(UserController user, AuthController auth)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== USER MENU  ===");
                Console.WriteLine("1. Book Ticket");
                Console.WriteLine("2. My Bookings");
                Console.WriteLine("3. Search Train By Number");
                Console.WriteLine("4. Available Trains");
                Console.WriteLine("5. Cancel Ticket");
                Console.WriteLine("0. Logout / Back");
                Console.Write("Choice: ");
                var ch = Console.ReadLine();
                switch (ch)
                {
                    case "1": user.BookTicketFlow(auth); break;
                    case "2": user.ShowMyBookings(auth); break;
                    case "3": user.SearchTrainFlow(); break;
                    case "4": user.ShowAvailableTrains(); break;
                    case "5": user.CancelTicketFlow(auth); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid. Press Enter."); Console.ReadLine(); break;
                }
            }
        }
    }
}
