using System;
using System.Collections.Generic;
using RailwayReservation.Models;
using RailwayReservation.Services;
using RailwayReservation.Views;

namespace RailwayReservation.Controllers
{
    public class UserController
    {
        private readonly BookingService _bookingSvc;
        private readonly TrainService _trainSvc;

        public UserController(BookingService b, TrainService t)
        {
            _bookingSvc = b; _trainSvc = t;
        }

        public void BookTicketFlow(AuthController auth)
        {
            Console.Clear();
            Console.WriteLine("=== BOOK TICKET ===");
            Console.WriteLine("Enter 0 at any time to go back.\n");

            var trains = _trainSvc.GetAllTrains();
            var rowsT = new List<string[]>();
            foreach (var tr in trains)
            {
                rowsT.Add(new[] { tr.TrainID.ToString(), tr.TrainNo, tr.TrainName, tr.SourceStationName, tr.DestinationStationName, tr.DepartureTime, tr.ArrivalTime, tr.AvailableSeats.ToString() });
            }
            TablePrinter.PrintTable(rowsT, new[] { "ID", "No", "Name", "Source", "Dest", "Dep", "Arr", "AvailSeats" });

            int trainId = PromptInt("Enter TrainID: "); if (trainId == -1) return;
            DateTime jdate = PromptDate("Journey Date (yyyy-MM-dd): "); if (jdate == DateTime.MinValue) return;
            string pname = PromptNonEmpty("Passenger Name: "); if (pname == null) return;
            int age = PromptPositiveInt("Passenger Age: "); if (age == -1) return;
            string gender = PromptNonEmpty("Passenger Gender: "); if (gender == null) return;
            string tclass = PromptClass("Travel Class (1AC/2AC/3AC/Sleeper): "); if (tclass == null) return;

            if (_bookingSvc.BookTicket(auth.LoggedInUserId, trainId, jdate, pname, age, gender, tclass, out string pnr, out string seat, out string msg))
            {
                Console.WriteLine($"Success! PNR: {pnr} Seat: {seat}");
            }
            else
            {
                Console.WriteLine("Failed: " + msg);
            }
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void ShowMyBookings(AuthController auth)
        {
            var list = _bookingSvc.GetBookingsByUser(auth.LoggedInUserId);
            var rows = new List<string[]>();
            foreach (var b in list)
            {
                rows.Add(new[] { b.BookingID.ToString(), b.PNR, b.TrainID.ToString(), b.PassengerName, b.TravelClass, b.SeatNumber, b.JourneyDate.ToString("yyyy-MM-dd") });
            }
            TablePrinter.PrintTable(rows, new[] { "BookingID", "PNR", "TrainID", "Passenger", "Class", "Seat", "JourneyDate" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void SearchTrainFlow()
        {
            Console.Write("Enter Train No to search or 0 to go back: ");
            string no = Console.ReadLine();
            if (no == "0") return;
            var t = _trainSvc.GetTrainByNo(no);
            if (t == null) { Console.WriteLine("Not found."); Console.ReadLine(); return; }
            var rows = new List<string[]>() {
                new[] { t.TrainID.ToString(), t.TrainNo, t.TrainName, t.SourceStationName, t.DestinationStationName, t.DepartureTime, t.ArrivalTime, t.AvailableDays }
            };
            TablePrinter.PrintTable(rows, new[] { "ID", "No", "Name", "Source", "Dest", "Dep", "Arr", "Days" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void ShowAvailableTrains()
        {
            var trains = _trainSvc.GetAllTrains();
            var rowsT = new List<string[]>();
            foreach (var tr in trains)
            {
                rowsT.Add(new[] { tr.TrainID.ToString(), tr.TrainNo, tr.TrainName, tr.SourceStationName, tr.DestinationStationName, tr.DepartureTime, tr.ArrivalTime, tr.AvailableSeats.ToString() });
            }
            TablePrinter.PrintTable(rowsT, new[] { "ID", "No", "Name", "Source", "Dest", "Dep", "Arr", "AvailSeats" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void CancelTicketFlow(AuthController auth)
        {
            Console.Write("Enter BookingID to cancel or 0 to go back: ");
            if (!int.TryParse(Console.ReadLine(), out int bid) || bid == 0) return;

            if (_bookingSvc.CancelBooking(bid, out decimal refund, out string msg))
            {
                Console.WriteLine(msg + $" Refund: {refund}");
            }
            else
            {
                Console.WriteLine("Cancel failed: " + msg);
            }
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        // Helpers
        private int PromptInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return -1;
                if (int.TryParse(s, out int v)) return v;
                Console.WriteLine("Enter valid integer or 0 to cancel.");
            }
        }

        private DateTime PromptDate(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return DateTime.MinValue;
                if (DateTime.TryParse(s, out DateTime d)) return d;
                Console.WriteLine("Enter valid date (yyyy-MM-dd) or 0 to cancel.");
            }
        }

        private string PromptNonEmpty(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return null;
                if (string.IsNullOrWhiteSpace(s)) { Console.WriteLine("Cannot be empty."); continue; }
                return s.Trim();
            }
        }

        private int PromptPositiveInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return -1;
                if (int.TryParse(s, out int v) && v > 0) return v;
                Console.WriteLine("Enter positive integer or 0 to cancel.");
            }
        }

        private string PromptClass(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return null;
                var upper = (s ?? "").ToUpper();
                if (upper == "1AC" || upper == "2AC" || upper == "3AC" || upper == "SLEEPER") return upper;
                Console.WriteLine("Class must be one of: 1AC,2AC,3AC,Sleeper or 0 to cancel.");
            }
        }
    }
}
