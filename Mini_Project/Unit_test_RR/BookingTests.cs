using NUnit.Framework;
using RailwayReservation.Services;
using NUnitAssert = NUnit.Framework.Assert;

using System;

namespace RailwayReservation.Tests
{
    [TestFixture]
    public class BookingTests
    {
        private BookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            string testConnStr = "your_test_database_connection_string";
            _bookingService = new BookingService(testConnStr);
        }

        [Test]
        public void BookTicket_ShouldReturnTrue_WhenSeatsAvailable()
        {
            // Arrange
            string seatNumber;
            string pnr;
            string msg;

            // Act
            var result = _bookingService.BookTicket(
                userId: 1,
                trainId: 101,
                journeyDate: DateTime.Now.AddDays(1),
                passengerName: "John Doe",
                age: 30,
                gender: "Male",
                travelClass: "Sleeper",
                pnr: out pnr,
                seatNumber: out seatNumber,
                msg: out msg
            );

            // Assert
            NUnitAssert.IsTrue(result, "Booking should succeed if seats are available.");
            NUnitAssert.IsNotNull(seatNumber, "Seat number should be assigned.");
            NUnitAssert.IsNotNull(pnr, "PNR should be generated.");
            NUnitAssert.AreEqual("Booking successful.", msg);

        }
    }
}
