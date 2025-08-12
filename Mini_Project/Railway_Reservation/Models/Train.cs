namespace RailwayReservation.Models
{
    public class Train
    {
        public int TrainID { get; set; }
        public string TrainNo { get; set; }
        public string TrainName { get; set; }
        public string SourceStationName { get; set; }
        public string DestinationStationName { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Status { get; set; }
        public string AvailableDays { get; set; }
        public int AvailableSeats_1AC { get; set; }
        public int AvailableSeats_2AC { get; set; }
        public int AvailableSeats_3AC { get; set; }
        public int AvailableSeats_Sleeper { get; set; }
        public int TotalSeats { get; set; }    // computed client side when needed
        public int AvailableSeats { get; set; } // computed client side
    }
}
