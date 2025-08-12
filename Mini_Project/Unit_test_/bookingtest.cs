using RailwayReservation.Services;
using System;
using Xunit;

namespace Unit_test_
{
    public class BookingTests
    {
        private readonly BookingService _bookingService;

        // Constructor acts 
        public BookingTests()
        {
            string testConnStr = "Server=ICS-LT-1JW37V3\\SQLEXPRESS;Database=RailwayDB;Trusted_Connection=True;";

            _bookingService = new BookingService(testConnStr);
        }

        [Fact] // Equivalent to [Test] in NUnit
        public void BookTicket_ShouldReturnTrue_WhenSeatsAvailable()
        {
            // Arrange
            string seatNumber;
            string pnr;
            string msg;

            // Act
            var result = _bookingService.BookTicket(
                userId: 2,
                trainId: 1,
                journeyDate: DateTime.Now.AddDays(1),
                passengerName: "rock",
                age: 30,
                gender: "Male",
                travelClass: "1AC",
                pnr: out pnr,
                seatNumber: out seatNumber,
                msg: out msg
            );

            // Assert 
            Assert.True(result, "Booking should succeed if seats are available.");
            Assert.NotNull(seatNumber);
            Assert.NotNull(pnr);
            Assert.Equal("Booking successful.", msg);
        }
    }
}
