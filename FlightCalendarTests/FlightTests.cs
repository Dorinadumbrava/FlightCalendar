using FlightCalendar;
using System;
using Xunit;

namespace FlightCalendarTests
{
    public class FlightTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidFlightNumberThrows(string flightNumber)
        {
            var flight = GetSut();

            Assert.Throws<ArgumentException>(() => flight.BortNumber = flightNumber);
        }

        [Theory]
        [InlineData("01:00:00")]
        [InlineData("02:59:59")]
        [InlineData("23:45:01")]
        public void InvalidStartTimeFlightThrows(string start)
        {
            var startTime = TimeSpan.Parse(start);
            var flight = GetSut();

            Assert.Throws<ArgumentException>(() => flight.Start = startTime);
        }

        [Theory]
        [InlineData("0:0:0")]
        [InlineData("0:4:59")]
        [InlineData("15:00:01")]
        public void InvalidFlightTakeOffDurationThrows(string durationString)
        {
            var startTime = new TimeSpan(15, 0, 0);
            var duration = TimeSpan.Parse(durationString);
            var flight = GetSut();
            flight.Start = startTime;

            Assert.Throws<ArgumentException>(() => flight.Duration = duration);
        }

        [Fact]
        public void SetsFlightNumberCorrectly()
        {
            var flight = GetSut();
            flight.BortNumber = "7E 667";

            Assert.Equal("7E 667", flight.BortNumber);
        }

        [Theory]
        [InlineData("03:00:00")]
        [InlineData("17:59:59")]
        [InlineData("23:45:00")]
        public void FlightSetsStartTimeCorrectly(string start)
        {
            var startTime = TimeSpan.Parse(start);
            var flight = GetSut();
            flight.Start = startTime;

            Assert.Equal(startTime, flight.Start);
        }

        [Theory]
        [InlineData("0:5:00")]
        [InlineData("0:15:0")]
        [InlineData("0:10:00")]
        public void FlightSetsEndTimeCorrectly(string durationstring)
        {
            var duration = TimeSpan.Parse(durationstring);
            var flight = GetSut();
            flight.Start = new TimeSpan(11, 0, 0);
            flight.Duration = duration;
            var expectedEndTime = new TimeSpan(11, 0, 0) + duration;

            Assert.Equal(expectedEndTime, flight.End);
        }

        [Theory]
        [InlineData("8:0:0", "0:10:0", true)]
        [InlineData("8:9:59", "0:10:0", true)]
        [InlineData("7:50:01", "0:10:0", true)]
        [InlineData("7:59:59", "0:10:02", true)]
        [InlineData("7:50:0", "0:10:00", false)]
        [InlineData("8:10:0", "0:10:00", false)]
        public void OverlapsReturnsExpected(string start, string durationstring, bool expected)
        {
            var duration = TimeSpan.Parse(durationstring);
            var startTime = TimeSpan.Parse(start);
            var flight = new Flight("NewFlight", startTime, duration);

            var existingFlight = GetSut();
            var actual = flight.Overlaps(existingFlight);

            Assert.Equal(expected, actual);
        }

        private static Flight GetSut()
        {
            return new Flight("TestFlight", new TimeSpan(8, 0, 0), new TimeSpan(0, 10, 0));
        }
    }
}