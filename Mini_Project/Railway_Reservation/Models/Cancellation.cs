using System;

namespace RailwayReservation.Models
{
    public class Cancellation
    {
        public int CancellationID { get; set; }
        public int BookingID { get; set; }
        public DateTime CancelledAt { get; set; }
        public decimal? RefundAmount { get; set; }
    }
}
