using System;

namespace FlightCalendar
{
    public static class TimeService
    {
        private static TimeSpan FlightDayStartTime = new TimeSpan(3, 0, 0);
        private static TimeSpan FlightDayEndTime = new TimeSpan(23, 45, 0);

        public static bool IsWithinWorkDay(TimeSpan time)
        {
            return time >= TimeService.FlightDayStartTime && time <= TimeService.FlightDayEndTime;
        }
    }
}