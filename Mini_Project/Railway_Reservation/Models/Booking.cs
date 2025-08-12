using System;

namespace RailwayReservation.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int TrainID { get; set; }
        public string SourceStationName { get; set; }
        public string DestinationStationName { get; set; }
        public DateTime JourneyDate { get; set; }
        public string PassengerName { get; set; }
        public int PassengerAge { get; set; }
        public string PassengerGender { get; set; }
        public string TravelClass { get; set; }
        public string SeatNumber { get; set; }
        public string PNR { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
