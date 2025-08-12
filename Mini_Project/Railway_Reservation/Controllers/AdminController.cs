using System;
using System.Collections.Generic;
using RailwayReservation.Models;
using RailwayReservation.Services;
using RailwayReservation.Views;

namespace RailwayReservation.Controllers
{
    public class AdminController
    {
        private readonly TrainService _trainSvc;
        private readonly StationService _stationSvc;
        private readonly AdminService _adminSvc;

        public AdminController(TrainService t, StationService s, AdminService a)
        {
            _trainSvc = t; _stationSvc = s; _adminSvc = a;
        }

        public void AddTrainFlow()
        {
            Console.Clear();
            Console.WriteLine("=== ADD TRAIN ===");
            Console.WriteLine("Enter 0 at any time to go back.\n");

            string trainNo = Prompt("Train No: ");
            if (trainNo == null) return;
            string trainName = Prompt("Train Name: ");
            if (trainName == null) return;

            var stations = _stationSvc.GetAllStations();
            if (stations.Count == 0)
            {
                Console.WriteLine("No stations available. Add stations first. Press Enter.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nStations:");
            var rows = new List<string[]>();
            foreach (var s in stations) rows.Add(new[] { s.StationID.ToString(), s.StationCode, s.StationName });
            TablePrinter.PrintTable(rows, new[] { "ID", "Code", "Name" });

            int src = PromptInt("Source Station ID: ");
            if (src == -1) return;
            int dst = PromptInt("Destination Station ID: ");
            if (dst == -1) return;
            string dep = Prompt("Departure Time (HH:mm): ");
            if (dep == null) return;
            string arr = Prompt("Arrival Time (HH:mm): ");
            if (arr == null) return;
            string days = Prompt("Available Days (Mon,Tue...): ");
            if (days == null) return;

            int s1 = PromptIntNonNegative("AvailableSeats 1AC: ");
            if (s1 == -1) return;
            int s2 = PromptIntNonNegative("AvailableSeats 2AC: ");
            if (s2 == -1) return;
            int s3 = PromptIntNonNegative("AvailableSeats 3AC: ");
            if (s3 == -1) return;
            int sl = PromptIntNonNegative("AvailableSeats Sleeper: ");
            if (sl == -1) return;

            var train = new Train
            {
                TrainNo = trainNo,
                TrainName = trainName,
                DepartureTime = dep,
                ArrivalTime = arr,
                Status = "running",
                AvailableDays = days,
                AvailableSeats_1AC = s1,
                AvailableSeats_2AC = s2,
                AvailableSeats_3AC = s3,
                AvailableSeats_Sleeper = sl
            };

            if (_trainSvc.AddTrain(train, src, dst, out string msg))
            {
                Console.WriteLine(msg);
            }
            else Console.WriteLine(msg);

            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        public void ModifyTrainFlow()
        {
            Console.Clear();
            Console.WriteLine("=== MODIFY TRAIN ===");
            Console.WriteLine("Enter Train No to modify or 0 to go back.");
            string no = Console.ReadLine();
            if (no == "0") return;
            var t = _trainSvc.GetTrainByNo(no);
            if (t == null)
            {
                Console.WriteLine("Train not found. Press Enter.");
                Console.ReadLine(); return;
            }

            Console.WriteLine($"Editing {t.TrainNo} - {t.TrainName} (press Enter to keep existing)");

            string name = PromptAllowEmpty($"Name ({t.TrainName}): ");
            string dep = PromptAllowEmpty($"DepartureTime ({t.DepartureTime}): ");
            string arr = PromptAllowEmpty($"ArrivalTime ({t.ArrivalTime}): ");
            string days = PromptAllowEmpty($"AvailableDays ({t.AvailableDays}): ");

            var stations = _stationSvc.GetAllStations();
            var rows = new List<string[]>();
            foreach (var s in stations) rows.Add(new[] { s.StationID.ToString(), s.StationCode, s.StationName });
            TablePrinter.PrintTable(rows, new[] { "ID", "Code", "Name" });

            int src = PromptIntAllowEmpty($"SourceStationID (current: {t.SourceStationName}) (Enter for keep): ", t.TrainID);
            if (src == int.MinValue) return; // back
            int dst = PromptIntAllowEmpty($"DestinationStationID (current: {t.DestinationStationName}) (Enter for keep): ", t.TrainID);
            if (dst == int.MinValue) return;

            int s1 = PromptIntAllowEmpty($"AvailableSeats_1AC ({t.AvailableSeats_1AC}): ", t.AvailableSeats_1AC);
            if (s1 == int.MinValue) return;
            int s2 = PromptIntAllowEmpty($"AvailableSeats_2AC ({t.AvailableSeats_2AC}): ", t.AvailableSeats_2AC);
            if (s2 == int.MinValue) return;
            int s3 = PromptIntAllowEmpty($"AvailableSeats_3AC ({t.AvailableSeats_3AC}): ", t.AvailableSeats_3AC);
            if (s3 == int.MinValue) return;
            int sl = PromptIntAllowEmpty($"AvailableSeats_Sleeper ({t.AvailableSeats_Sleeper}): ", t.AvailableSeats_Sleeper);
            if (sl == int.MinValue) return;

            var newTrain = new Train
            {
                TrainNo = t.TrainNo,
                TrainName = string.IsNullOrEmpty(name) ? t.TrainName : name,
                DepartureTime = string.IsNullOrEmpty(dep) ? t.DepartureTime : dep,
                ArrivalTime = string.IsNullOrEmpty(arr) ? t.ArrivalTime : arr,
                AvailableDays = string.IsNullOrEmpty(days) ? t.AvailableDays : days,
                AvailableSeats_1AC = s1 == int.MinValue ? t.AvailableSeats_1AC : s1,
                AvailableSeats_2AC = s2 == int.MinValue ? t.AvailableSeats_2AC : s2,
                AvailableSeats_3AC = s3 == int.MinValue ? t.AvailableSeats_3AC : s3,
                AvailableSeats_Sleeper = sl == int.MinValue ? t.AvailableSeats_Sleeper : sl,
                Status = t.Status
            };

            if (_trainSvc.UpdateTrain(newTrain, t.TrainID, src == int.MinValue ? t.TrainID : src, dst == int.MinValue ? t.TrainID : dst, out string msg))
            {
                Console.WriteLine(msg);
            }
            else Console.WriteLine(msg);

            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        public void DeleteTrainFlow()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE TRAIN ===");
            Console.Write("Enter TrainID to delete or 0 to go back: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

            if (_trainSvc.DeleteTrain(id, out string msg))
                Console.WriteLine(msg);
            else Console.WriteLine(msg);

            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        public void ManageStationsFlow()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== STATIONS MANAGEMENT ===");
                Console.WriteLine("1. Add Station");
                Console.WriteLine("2. Modify Station");
                Console.WriteLine("3. Delete Station");
                Console.WriteLine("4. List Stations");
                Console.WriteLine("0. Back");
                Console.Write("Choice: ");
                var ch = Console.ReadLine();
                if (ch == "1")
                {
                    Console.Write("Code: "); string code = Console.ReadLine();
                    if (code == "0") continue;
                    Console.Write("Name: "); string name = Console.ReadLine();
                    if (name == "0") continue;
                    if (_stationSvc.AddStation(code, name, out string msg)) Console.WriteLine(msg); else Console.WriteLine(msg);
                    Console.WriteLine("Press Enter."); Console.ReadLine();
                }
                else if (ch == "2")
                {
                    Console.Write("StationID to modify: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid."); Console.ReadLine(); continue; }
                    Console.Write("New Code: "); string code = Console.ReadLine();
                    Console.Write("New Name: "); string name = Console.ReadLine();
                    if (_stationSvc.UpdateStation(id, code, name, out string msg)) Console.WriteLine(msg); else Console.WriteLine(msg);
                    Console.WriteLine("Press Enter."); Console.ReadLine();
                }
                else if (ch == "3")
                {
                    Console.Write("StationID to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid."); Console.ReadLine(); continue; }
                    if (_stationSvc.DeleteStation(id, out string msg)) Console.WriteLine(msg); else Console.WriteLine(msg);
                    Console.WriteLine("Press Enter."); Console.ReadLine();
                }
                else if (ch == "4")
                {
                    var stations = _stationSvc.GetAllStations();
                    var rows = new List<string[]>();
                    foreach (var s in stations) rows.Add(new[] { s.StationID.ToString(), s.StationCode, s.StationName });
                    TablePrinter.PrintTable(rows, new[] { "ID", "Code", "Name" });
                    Console.WriteLine("Press Enter."); Console.ReadLine();
                }
                else if (ch == "0") return;
                else { Console.WriteLine("Invalid."); Console.ReadLine(); }
            }
        }
        public void ShowAllTrains()
        {
            var trains = _trainSvc.GetAllTrains();
            var rows = new List<string[]>();

            foreach (var t in trains)
            {
                rows.Add(new[]
                {
            t.TrainID.ToString(),
            t.TrainNo,
            t.TrainName,
            t.SourceStationName,
            t.DestinationStationName,
            t.Status,
            t.AvailableDays,
            t.AvailableSeats.ToString(),
            t.AvailableSeats_1AC.ToString(),
            t.AvailableSeats_2AC.ToString(),
            t.AvailableSeats_3AC.ToString(),
            t.AvailableSeats_Sleeper.ToString(),
            t.DepartureTime.ToString(),
            t.ArrivalTime.ToString()
        });
            }

            TablePrinter.PrintTable(rows, new[] { "TrainID", "Train No", "Train Name", "Source", "Destination", "Status", "Available Days", "Seats", "1AC", "2AC", "3AC", "Sleeper", "Departure", "Arrival" });

            Console.Write("\nDo you want to change the status of a train? (y/n): ");
            var ans = Console.ReadLine()?.Trim().ToLower();
            if (ans == "y")
            {
                Console.Write("Enter Train ID: ");
                if (int.TryParse(Console.ReadLine(), out int trainId))
                {
                    Console.Write("Enter new status (running / cancelled / maintenance): ");
                    string status = Console.ReadLine()?.Trim().ToLower();

                    if (status == "running" || status == "cancelled" || status == "maintenance")
                    {
                        string msg;
                        if (_trainSvc.UpdateTrainStatus(trainId, status, out  msg))
                        {
                            Console.WriteLine(msg);
                        }
                        else
                        {
                            Console.WriteLine(msg);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid status entered.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Train ID.");
                }
            }

            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();
        }

        public void ShowAllBookings()
        {
            var bs = _adminSvc.GetAllBookings();
            var rows = new List<string[]>();
            foreach (var b in bs)
            {
                rows.Add(new[] {
                    b.BookingID.ToString(), b.UserID.ToString(), b.PNR, b.PassengerName, b.TravelClass, b.SeatNumber,
                    b.SourceStationName, b.DestinationStationName, b.JourneyDate.ToString("yyyy-MM-dd"), b.BookingDate.ToString("yyyy-MM-dd")
                });
            }
            TablePrinter.PrintTable(rows, new[] { "BookingID", "UserID", "PNR", "Passenger", "Class", "Seat", "Source", "Dest", "JourneyDate", "BookedOn" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void ShowAllUsers()
        {
            var us = _adminSvc.GetAllUsers();
            var rows = new List<string[]>();
            foreach (var u in us)
            {
                rows.Add(new[] { u.UserID.ToString(), u.Username, u.FullName, u.Email, u.Phone, u.Role, u.Status });
            }
            TablePrinter.PrintTable(rows, new[] { "ID", "Username", "FullName", "Email", "Phone", "Role", "Status" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        public void ShowAllCancellations()
        {
            var cs = _adminSvc.GetAllCancellations();
            var rows = new List<string[]>();
            foreach (var c in cs)
            {
                rows.Add(new[] { c.CancellationID.ToString(), c.BookingID.ToString(), c.CancelledAt.ToString("yyyy-MM-dd HH:mm"), (c.RefundAmount ?? 0).ToString() });
            }
            TablePrinter.PrintTable(rows, new[] { "CancelID", "BookingID", "CancelledAt", "Refund" });
            Console.WriteLine("Press Enter."); Console.ReadLine();
        }

        // helpers
        private string Prompt(string label)
        {
            Console.Write(label);
            var s = Console.ReadLine();
            if (s == "0") return null;
            if (string.IsNullOrWhiteSpace(s)) return "";
            return s.Trim();
        }

        private string PromptAllowEmpty(string label)
        {
            Console.Write(label);
            var s = Console.ReadLine();
            if (s == "0") return null;
            return s;
        }

        private int PromptInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return -1;
                if (int.TryParse(s, out int v)) return v;
                Console.WriteLine("Enter a valid integer or 0 to cancel.");
            }
        }

        private int PromptIntNonNegative(string label)
        {
            while (true)
            {
                Console.Write(label);
                var s = Console.ReadLine();
                if (s == "0") return -1;
                if (int.TryParse(s, out int v) && v >= 0) return v;
                Console.WriteLine("Enter valid non-negative integer or 0 to cancel.");
            }
        }

        private int PromptIntAllowEmpty(string label, int fallback)
        {
            Console.Write(label);
            var s = Console.ReadLine();
            if (s == "0") return int.MinValue;
            if (string.IsNullOrWhiteSpace(s)) return fallback;
            if (int.TryParse(s, out int v)) return v;
            return fallback;
        }
    }
}
