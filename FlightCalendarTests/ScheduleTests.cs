using FlightCalendar;
using System;
using Xunit;

namespace FlightCalendarTests
{
    public class FlightScheduleTests
    {
        [Fact]
        public void ScheduleInnitiatesCorrectly()
        {
            var date = DateTime.Now;
            var schedule = new Schedule(date);

            Assert.Equal(date, schedule.Date);
            Assert.Empty(schedule.Flights);
        }

        [Fact]
        public void TryAddFlightNullMeetingReturnsFalse()
        {
            var schedule = new Schedule(DateTime.Now);

            Assert.Throws<ArgumentNullException>(() => schedule.TryAddFlight(null, out string error));
        }

        [Theory]
        [InlineData("9U 738", "10:12:0", "0:15:0", "Overlaping flight: 9U 738", false)]
        [InlineData("4R 887", "12:0:01", "0:10:00", "", true)]
        public void TryAddInvalidFlightReturnsFalse(string topic, string start, string duration, string expectedError, bool expectedResult)
        {
            string error = "";
            var schedule = new Schedule(DateTime.Now);
            var scheduledFlight = new Flight("10E 665", new TimeSpan(10, 15, 0), new TimeSpan(0, 10, 0));
            schedule.TryAddFlight(scheduledFlight, out error);
            var meeting = new Flight(topic, TimeSpan.Parse(start), TimeSpan.Parse(duration));

            var result = schedule.TryAddFlight(meeting, out error);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedError, error);
        }
    }
}