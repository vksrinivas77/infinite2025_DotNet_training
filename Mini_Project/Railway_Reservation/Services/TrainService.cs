using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.Services
{
    public class TrainService
    {
        private readonly string _conn;
        public TrainService(string connStr) { _conn = connStr; }

        public bool AddTrain(Train t, int sourceStationId, int destStationId, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(@"INSERT INTO Trains 
(TrainNo, TrainName, SourceStationID, DestinationStationID, DepartureTime, ArrivalTime, Status, AvailableDays,
 AvailableSeats_1AC, AvailableSeats_2AC, AvailableSeats_3AC, AvailableSeats_Sleeper)
VALUES (@no,@name,@src,@dst,@dep,@arr,@status,@days,@s1,@s2,@s3,@sl)", conn))
                {
                    cmd.Parameters.AddWithValue("@no", t.TrainNo);
                    cmd.Parameters.AddWithValue("@name", t.TrainName);
                    cmd.Parameters.AddWithValue("@src", sourceStationId);
                    cmd.Parameters.AddWithValue("@dst", destStationId);
                    cmd.Parameters.AddWithValue("@dep", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@arr", t.ArrivalTime);
                    cmd.Parameters.AddWithValue("@status", t.Status ?? "running");
                    cmd.Parameters.AddWithValue("@days", t.AvailableDays ?? "");
                    cmd.Parameters.AddWithValue("@s1", t.AvailableSeats_1AC);
                    cmd.Parameters.AddWithValue("@s2", t.AvailableSeats_2AC);
                    cmd.Parameters.AddWithValue("@s3", t.AvailableSeats_3AC);
                    cmd.Parameters.AddWithValue("@sl", t.AvailableSeats_Sleeper);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    msg = "Train added.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "AddTrain error: " + ex.Message;
                return false;
            }
        }

        public bool UpdateTrain(Train t, int trainId, int sourceStationId, int destStationId, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(@"UPDATE Trains SET TrainNo=@no, TrainName=@name, SourceStationID=@src, DestinationStationID=@dst,
DepartureTime=@dep, ArrivalTime=@arr, Status=@status, AvailableDays=@days, AvailableSeats_1AC=@s1, AvailableSeats_2AC=@s2,
AvailableSeats_3AC=@s3, AvailableSeats_Sleeper=@sl WHERE TrainID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@no", t.TrainNo);
                    cmd.Parameters.AddWithValue("@name", t.TrainName);
                    cmd.Parameters.AddWithValue("@src", sourceStationId);
                    cmd.Parameters.AddWithValue("@dst", destStationId);
                    cmd.Parameters.AddWithValue("@dep", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@arr", t.ArrivalTime);
                    cmd.Parameters.AddWithValue("@status", t.Status ?? "running");
                    cmd.Parameters.AddWithValue("@days", t.AvailableDays ?? "");
                    cmd.Parameters.AddWithValue("@s1", t.AvailableSeats_1AC);
                    cmd.Parameters.AddWithValue("@s2", t.AvailableSeats_2AC);
                    cmd.Parameters.AddWithValue("@s3", t.AvailableSeats_3AC);
                    cmd.Parameters.AddWithValue("@sl", t.AvailableSeats_Sleeper);
                    cmd.Parameters.AddWithValue("@id", trainId);
                    conn.Open();
                    int aff = cmd.ExecuteNonQuery();
                    msg = aff > 0 ? "Train updated." : "Train not found.";
                    return aff > 0;
                }
            }
            catch (Exception ex)
            {
                msg = "UpdateTrain error: " + ex.Message;
                return false;
            }
        }

        public bool DeleteTrain(int trainId, out string msg)
        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("DELETE FROM Trains WHERE TrainID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", trainId);
                    conn.Open();
                    int aff = cmd.ExecuteNonQuery();
                    msg = aff > 0 ? "Train deleted." : "Train not found or in use.";
                    return aff > 0;
                }
            }
            catch (Exception ex)
            {
                msg = "DeleteTrain error: " + ex.Message;
                return false;
            }
        }
        public bool UpdateTrainStatus(int trainId, string newStatus, out string msg)

        {
            msg = "";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand("UPDATE Trains SET Status = @status WHERE TrainID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", trainId);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating train status: " + ex.Message);
                return false;
            }
        }

        public List<Train> GetAllTrains()
        {
            var list = new List<Train>();
            try
            {
                string sql = @"
SELECT t.TrainID, t.TrainNo, t.TrainName, s1.StationName AS SourceName, s2.StationName AS DestName,
CONVERT(varchar(8), t.DepartureTime, 108) AS DepartureTime, CONVERT(varchar(8), t.ArrivalTime, 108) AS ArrivalTime,
t.Status, t.AvailableDays, t.AvailableSeats_1AC, t.AvailableSeats_2AC, t.AvailableSeats_3AC, t.AvailableSeats_Sleeper
FROM Trains t
LEFT JOIN Stations s1 ON t.SourceStationID = s1.StationID
LEFT JOIN Stations s2 ON t.DestinationStationID = s2.StationID
ORDER BY t.TrainName";

                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var tr = new Train
                            {
                                TrainID = Convert.ToInt32(rdr["TrainID"]),
                                TrainNo = rdr["TrainNo"].ToString(),
                                TrainName = rdr["TrainName"].ToString(),
                                SourceStationName = rdr["SourceName"].ToString(),
                                DestinationStationName = rdr["DestName"].ToString(),
                                DepartureTime = rdr["DepartureTime"].ToString(),
                                ArrivalTime = rdr["ArrivalTime"].ToString(),
                                Status = rdr["Status"].ToString(),
                                AvailableDays = rdr["AvailableDays"].ToString(),
                                AvailableSeats_1AC = Convert.ToInt32(rdr["AvailableSeats_1AC"]),
                                AvailableSeats_2AC = Convert.ToInt32(rdr["AvailableSeats_2AC"]),
                                AvailableSeats_3AC = Convert.ToInt32(rdr["AvailableSeats_3AC"]),
                                AvailableSeats_Sleeper = Convert.ToInt32(rdr["AvailableSeats_Sleeper"])
                            };
                            tr.TotalSeats = tr.AvailableSeats_1AC + tr.AvailableSeats_2AC + tr.AvailableSeats_3AC + tr.AvailableSeats_Sleeper;
                            tr.AvailableSeats = tr.TotalSeats;
                            list.Add(tr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllTrains error: " + ex.Message);
            }
            return list;
        }

        public Train GetTrainByNo(string trainNo)
        {
            try
            {
                string sql = @"
SELECT t.TrainID, t.TrainNo, t.TrainName, s1.StationName AS SourceName, s2.StationName AS DestName,
CONVERT(varchar(8), t.DepartureTime, 108) AS DepartureTime, CONVERT(varchar(8), t.ArrivalTime, 108) AS ArrivalTime,
t.Status, t.AvailableDays, t.AvailableSeats_1AC, t.AvailableSeats_2AC, t.AvailableSeats_3AC, t.AvailableSeats_Sleeper
FROM Trains t
LEFT JOIN Stations s1 ON t.SourceStationID = s1.StationID
LEFT JOIN Stations s2 ON t.DestinationStationID = s2.StationID
WHERE t.TrainNo = @no";

                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@no", trainNo);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            var tr = new Train
                            {
                                TrainID = Convert.ToInt32(rdr["TrainID"]),
                                TrainNo = rdr["TrainNo"].ToString(),
                                TrainName = rdr["TrainName"].ToString(),
                                SourceStationName = rdr["SourceName"].ToString(),
                                DestinationStationName = rdr["DestName"].ToString(),
                                DepartureTime = rdr["DepartureTime"].ToString(),
                                ArrivalTime = rdr["ArrivalTime"].ToString(),
                                Status = rdr["Status"].ToString(),
                                AvailableDays = rdr["AvailableDays"].ToString(),
                                AvailableSeats_1AC = Convert.ToInt32(rdr["AvailableSeats_1AC"]),
                                AvailableSeats_2AC = Convert.ToInt32(rdr["AvailableSeats_2AC"]),
                                AvailableSeats_3AC = Convert.ToInt32(rdr["AvailableSeats_3AC"]),
                                AvailableSeats_Sleeper = Convert.ToInt32(rdr["AvailableSeats_Sleeper"])
                            };
                            tr.TotalSeats = tr.AvailableSeats_1AC + tr.AvailableSeats_2AC + tr.AvailableSeats_3AC + tr.AvailableSeats_Sleeper;
                            tr.AvailableSeats = tr.TotalSeats;
                            return tr;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTrainByNo error: " + ex.Message);
            }
            return null;
        }

        public Train GetTrainById(int trainId)
        {
            try
            {
                string sql = @"
SELECT t.TrainID, t.TrainNo, t.TrainName, s1.StationName AS SourceName, s2.StationName AS DestName,
CONVERT(varchar(8), t.DepartureTime, 108) AS DepartureTime, CONVERT(varchar(8), t.ArrivalTime, 108) AS ArrivalTime,
t.Status, t.AvailableDays, t.AvailableSeats_1AC, t.AvailableSeats_2AC, t.AvailableSeats_3AC, t.AvailableSeats_Sleeper
FROM Trains t
LEFT JOIN Stations s1 ON t.SourceStationID = s1.StationID
LEFT JOIN Stations s2 ON t.DestinationStationID = s2.StationID
WHERE t.TrainID = @id";

                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", trainId);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            var tr = new Train
                            {
                                TrainID = Convert.ToInt32(rdr["TrainID"]),
                                TrainNo = rdr["TrainNo"].ToString(),
                                TrainName = rdr["TrainName"].ToString(),
                                SourceStationName = rdr["SourceName"].ToString(),
                                DestinationStationName = rdr["DestName"].ToString(),
                                DepartureTime = rdr["DepartureTime"].ToString(),
                                ArrivalTime = rdr["ArrivalTime"].ToString(),
                                Status = rdr["Status"].ToString(),
                                AvailableDays = rdr["AvailableDays"].ToString(),
                                AvailableSeats_1AC = Convert.ToInt32(rdr["AvailableSeats_1AC"]),
                                AvailableSeats_2AC = Convert.ToInt32(rdr["AvailableSeats_2AC"]),
                                AvailableSeats_3AC = Convert.ToInt32(rdr["AvailableSeats_3AC"]),
                                AvailableSeats_Sleeper = Convert.ToInt32(rdr["AvailableSeats_Sleeper"])
                            };
                            tr.TotalSeats = tr.AvailableSeats_1AC + tr.AvailableSeats_2AC + tr.AvailableSeats_3AC + tr.AvailableSeats_Sleeper;
                            tr.AvailableSeats = tr.TotalSeats;
                            return tr;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTrainById error: " + ex.Message);
            }
            return null;
        }
    }
}
