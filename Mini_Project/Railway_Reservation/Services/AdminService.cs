using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.Services
{
    public class AdminService
    {
        private readonly string _conn;
        public AdminService(string connStr) { _conn = connStr; }

        public List<User> GetAllUsers()
        {
            var list = new List<User>();
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("SELECT UserID, Username, FullName, Email, Phone, Role, Status FROM Users ORDER BY Username", conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new User
                            {
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                Username = rdr["Username"].ToString(),
                                FullName = rdr["FullName"].ToString(),
                                Email = rdr["Email"].ToString(),
                                Phone = rdr["Phone"].ToString(),
                                Role = rdr["Role"].ToString(),
                                Status = rdr["Status"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllUsers error: " + ex.Message);
            }
            return list;
        }

        public List<Booking> GetAllBookings()
        {
            var bs = new BookingService(_conn);
            return bs.GetAllBookings();
        }

        public List<Cancellation> GetAllCancellations()
        {
            var list = new List<Cancellation>();
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("SELECT CancellationID, BookingID, CancelledAt, RefundAmount FROM Cancellations ORDER BY CancelledAt DESC", conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new Cancellation
                            {
                                CancellationID = Convert.ToInt32(rdr["CancellationID"]),
                                BookingID = Convert.ToInt32(rdr["BookingID"]),
                                CancelledAt = Convert.ToDateTime(rdr["CancelledAt"]),
                                RefundAmount = rdr["RefundAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["RefundAmount"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllCancellations error: " + ex.Message);
            }
            return list;
        }
    }
}
