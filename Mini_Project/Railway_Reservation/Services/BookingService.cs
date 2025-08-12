using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.Services
{
    public class BookingService
    {
        private readonly string _conn;
        public BookingService(string connStr) { _conn = connStr; }

        // Book ticket with transactional logic (inline)
        public bool BookTicket(int userId, int trainId, DateTime journeyDate, string passengerName, int age, string gender, string travelClass, out string pnr, out string seatNumber, out string msg)
        {
            pnr = null; seatNumber = null; msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. check availability
                            string col = ClassToColumn(travelClass);
                            if (col == null) { msg = "Invalid travel class."; return false; }

                            string availSql = $"SELECT {col} FROM Trains WITH (UPDLOCK, ROWLOCK) WHERE TrainID=@tid";
                            using (var cmdCheck = new SqlCommand(availSql, conn, tran))
                            {
                                cmdCheck.Parameters.AddWithValue("@tid", trainId);
                                object o = cmdCheck.ExecuteScalar();
                                if (o == null) { msg = "Train not found."; tran.Rollback(); return false; }
                                int avail = Convert.ToInt32(o);
                                if (avail <= 0) { msg = "No seats available in selected class."; tran.Rollback(); return false; }
                            }

                            // 2. decrement availability
                            string decSql = $"UPDATE Trains SET {col} = {col} - 1 WHERE TrainID=@tid";
                            using (var cmdDec = new SqlCommand(decSql, conn, tran))
                            {
                                cmdDec.Parameters.AddWithValue("@tid", trainId);
                                cmdDec.ExecuteNonQuery();
                            }

                            // 3. generate seat number and pnr
                            seatNumber = travelClass + "-" + (new Random().Next(1, 1000)).ToString("D3");
                            pnr = "PNR" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999).ToString();

                            // 4. insert booking (source/dest captured from train join)
                            string insertSql = @"
INSERT INTO Bookings (UserID, TrainID, SourceStationName, DestinationStationName, JourneyDate, PassengerName, PassengerAge, PassengerGender, TravelClass, SeatNumber, PNR)
VALUES (@uid, @tid,
    (SELECT s1.StationName FROM Stations s1 JOIN Trains t ON t.SourceStationID=s1.StationID WHERE t.TrainID=@tid),
    (SELECT s2.StationName FROM Stations s2 JOIN Trains t2 ON t2.DestinationStationID=s2.StationID WHERE t2.TrainID=@tid),
    @jdate, @pname, @age, @gender, @tclass, @seat, @pnr)";
                            using (var cmdIns = new SqlCommand(insertSql, conn, tran))
                            {
                                cmdIns.Parameters.AddWithValue("@uid", userId);
                                cmdIns.Parameters.AddWithValue("@tid", trainId);
                                cmdIns.Parameters.AddWithValue("@jdate", journeyDate.Date);
                                cmdIns.Parameters.AddWithValue("@pname", passengerName);
                                cmdIns.Parameters.AddWithValue("@age", age);
                                cmdIns.Parameters.AddWithValue("@gender", gender ?? (object)DBNull.Value);
                                cmdIns.Parameters.AddWithValue("@tclass", travelClass);
                                cmdIns.Parameters.AddWithValue("@seat", seatNumber);
                                cmdIns.Parameters.AddWithValue("@pnr", pnr);
                                cmdIns.ExecuteNonQuery();
                            }

                            tran.Commit();
                            msg = "Booking successful.";
                            return true;
                        }
                        catch (Exception exInner)
                        {
                            tran.Rollback();
                            msg = "Booking failed: " + exInner.Message;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Booking error: " + ex.Message;
                return false;
            }
        }

        private string ClassToColumn(string travelClass)
        {
            switch ((travelClass ?? "").ToUpper())
            {
                case "1AC": return "AvailableSeats_1AC";
                case "2AC": return "AvailableSeats_2AC";
                case "3AC": return "AvailableSeats_3AC";
                case "SLEEPER": return "AvailableSeats_Sleeper";
                default: return null;
            }
        }

        public List<Booking> GetBookingsByUser(int userId)
        {
            var list = new List<Booking>();
            try
            {
                string sql = @"SELECT BookingID, UserID, TrainID, SourceStationName, DestinationStationName, JourneyDate, PassengerName, PassengerAge, PassengerGender, TravelClass, SeatNumber, PNR, BookingDate
FROM Bookings WHERE UserID=@uid ORDER BY BookingDate DESC";
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new Booking
                            {
                                BookingID = Convert.ToInt32(rdr["BookingID"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                TrainID = Convert.ToInt32(rdr["TrainID"]),
                                SourceStationName = rdr["SourceStationName"].ToString(),
                                DestinationStationName = rdr["DestinationStationName"].ToString(),
                                JourneyDate = Convert.ToDateTime(rdr["JourneyDate"]),
                                PassengerName = rdr["PassengerName"].ToString(),
                                PassengerAge = Convert.ToInt32(rdr["PassengerAge"]),
                                PassengerGender = rdr["PassengerGender"].ToString(),
                                TravelClass = rdr["TravelClass"].ToString(),
                                SeatNumber = rdr["SeatNumber"].ToString(),
                                PNR = rdr["PNR"].ToString(),
                                BookingDate = Convert.ToDateTime(rdr["BookingDate"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetBookingsByUser error: " + ex.Message);
            }
            return list;
        }

        public List<Booking> GetAllBookings()
        {
            var list = new List<Booking>();
            try
            {
                string sql = @"SELECT BookingID, UserID, TrainID, SourceStationName, DestinationStationName, JourneyDate, PassengerName, PassengerAge, PassengerGender, TravelClass, SeatNumber, PNR, BookingDate
FROM Bookings ORDER BY BookingDate DESC";
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new Booking
                            {
                                BookingID = Convert.ToInt32(rdr["BookingID"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                TrainID = Convert.ToInt32(rdr["TrainID"]),
                                SourceStationName = rdr["SourceStationName"].ToString(),
                                DestinationStationName = rdr["DestinationStationName"].ToString(),
                                JourneyDate = Convert.ToDateTime(rdr["JourneyDate"]),
                                PassengerName = rdr["PassengerName"].ToString(),
                                PassengerAge = Convert.ToInt32(rdr["PassengerAge"]),
                                PassengerGender = rdr["PassengerGender"].ToString(),
                                TravelClass = rdr["TravelClass"].ToString(),
                                SeatNumber = rdr["SeatNumber"].ToString(),
                                PNR = rdr["PNR"].ToString(),
                                BookingDate = Convert.ToDateTime(rdr["BookingDate"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllBookings error: " + ex.Message);
            }
            return list;
        }

        public bool CancelBooking(int bookingId, out decimal refundAmount, out string msg)
        {
            refundAmount = 0m; msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // get current booking and travel class, train id
                            int trainId = 0;
                            string tclass = null;
                            using (var cmd = new SqlCommand("SELECT TrainID, TravelClass FROM Bookings WHERE BookingID=@bid", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@bid", bookingId);
                                using (var rdr = cmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                        trainId = Convert.ToInt32(rdr["TrainID"]);
                                        tclass = rdr["TravelClass"].ToString();
                                    }
                                    else
                                    {
                                        msg = "Booking not found.";
                                        tran.Rollback();
                                        return false;
                                    }
                                }
                            }

                            // insert cancellation record
                            using (var ins = new SqlCommand("INSERT INTO Cancellations (BookingID, CancelledAt, RefundAmount) VALUES (@bid, GETDATE(), @r)", conn, tran))
                            {
                                ins.Parameters.AddWithValue("@bid", bookingId);
                                ins.Parameters.AddWithValue("@r", 0.00m);
                                ins.ExecuteNonQuery();
                            }

                            // increment availability
                            string col = ClassToColumn(tclass);
                            if (col == null) { msg = "Invalid class in booking."; tran.Rollback(); return false; }
                            using (var upd = new SqlCommand($"UPDATE Trains SET {col} = {col} + 1 WHERE TrainID=@tid", conn, tran))
                            {
                                upd.Parameters.AddWithValue("@tid", trainId);
                                upd.ExecuteNonQuery();
                            }

                            // compute refund amount based on business rules (here 0)
                            refundAmount = 0m;

                            tran.Commit();
                            msg = "Cancellation successful.";
                            return true;
                        }
                        catch (Exception exInner)
                        {
                            tran.Rollback();
                            msg = "Cancel failed: " + exInner.Message;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Cancel error: " + ex.Message;
                return false;
            }
        }
    }
}
