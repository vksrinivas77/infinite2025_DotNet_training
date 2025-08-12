using System;
using System.Configuration;
using RailwayReservation.Controllers;
using RailwayReservation.Services;
using RailwayReservation.Views;

namespace RailwayReservation
{
    class Program
    {
        static void Main(string[] args)
        {
            string conn = ConfigurationManager.ConnectionStrings["RailwayDB"].ConnectionString;

            // Services
            var authService = new AuthService(conn);
            var trainService = new TrainService(conn);
            var stationService = new StationService(conn);
            var bookingService = new BookingService(conn);
            var adminService = new AdminService(conn);

            // Controllers
            var authCtrl = new AuthController(authService);
            var adminCtrl = new AdminController(trainService, stationService, adminService);
            var userCtrl = new UserController(bookingService, trainService);

            MenuService.ShowMainMenu(authCtrl, adminCtrl, userCtrl);
        }
    }
}
