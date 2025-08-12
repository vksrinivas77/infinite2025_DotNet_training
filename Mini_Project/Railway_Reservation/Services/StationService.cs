using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.Services
{
    public class StationService
    {
        private readonly string _conn;
        public StationService(string connStr) { _conn = connStr; }

        public bool AddStation(string code, string name, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("INSERT INTO Stations (StationCode, StationName) VALUES (@code, @name)", conn))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@name", name);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    msg = "Station added.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "AddStation error: " + ex.Message;
                return false;
            }
        }

        public bool UpdateStation(int id, string code, string name, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("UPDATE Stations SET StationCode=@code, StationName=@name WHERE StationID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int aff = cmd.ExecuteNonQuery();
                    msg = aff > 0 ? "Station updated." : "Station not found.";
                    return aff > 0;
                }
            }
            catch (Exception ex)
            {
                msg = "UpdateStation error: " + ex.Message;
                return false;
            }
        }

        public bool DeleteStation(int id, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("DELETE FROM Stations WHERE StationID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int aff = cmd.ExecuteNonQuery();
                    msg = aff > 0 ? "Station deleted." : "Station not found or in use.";
                    return aff > 0;
                }
            }
            catch (Exception ex)
            {
                msg = "DeleteStation error: " + ex.Message;
                return false;
            }
        }

        public List<Station> GetAllStations()
        {
            var list = new List<Station>();
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("SELECT StationID, StationCode, StationName FROM Stations ORDER BY StationName", conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new Station
                            {
                                StationID = Convert.ToInt32(rdr["StationID"]),
                                StationCode = rdr["StationCode"].ToString(),
                                StationName = rdr["StationName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllStations error: " + ex.Message);
            }
            return list;
        }
    }
}
