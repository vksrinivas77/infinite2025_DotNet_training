using System;
using System.Data.SqlClient;

namespace RailwayReservation.Services
{
    public class AuthService
    {
        private readonly string _conn;
        public AuthService(string connectionString) { _conn = connectionString; }

        public bool ValidateLogin(string username, string password, string role, out int userId, out string fullName)
        {
            userId = 0; fullName = null;
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("SELECT UserID, FullName FROM Users WHERE Username=@u AND Password=@p AND Role=@r AND Status='active'", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    cmd.Parameters.AddWithValue("@r", role);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            userId = Convert.ToInt32(rdr["UserID"]);
                            fullName = rdr["FullName"].ToString();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Auth error: " + ex.Message);
            }
            return false;
        }

        public bool RegisterUser(string username, string password, string fullname, string email, string phone, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var check = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username=@u", conn))
                {
                    check.Parameters.AddWithValue("@u", username);
                    conn.Open();
                    var exists = (int)check.ExecuteScalar();
                    if (exists > 0) { msg = "Username already exists."; return false; }

                    using (var insert = new SqlCommand(
                        "INSERT INTO Users (Username, Password, Role, Status, FullName, Email, Phone) VALUES (@u,@p,'user','active',@f,@e,@ph)", conn))
                    {
                        insert.Parameters.AddWithValue("@u", username);
                        insert.Parameters.AddWithValue("@p", password);
                        insert.Parameters.AddWithValue("@f", fullname);
                        insert.Parameters.AddWithValue("@e", email);
                        insert.Parameters.AddWithValue("@ph", phone);
                        insert.ExecuteNonQuery();
                        msg = "Registered successfully.";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Register error: " + ex.Message;
            }
            return false;
        }
    }
}
