using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightCalendar
{
    //Flights should not overlap.
    public class Schedule
    {
        public Schedule(DateTime date)
        {
            Date = date;
            Flights = new List<Flight>();
        }
        public DateTime Date { get; set; }
        public List<Flight> Flights { get; }

        //UseResult
        public bool TryAddFlight(Flight flight, out string error)
        {
            error = "";
            if (flight is null)
            {
                throw new
                    ArgumentNullException("Can not add flight to schedule: flight is null", "flight");
            }
            if (Flights.Any(x => flight.Overlaps(x)))
            {
                error = $"Overlaping flight: {flight.BortNumber}";
                return false;
            }
            Flights.Add(flight);
            return true;
        }

        public void Show()
        {
            Console.WriteLine($"Flight schedule for {Date}. Scheduled flights:");
            foreach (var flight in Flights)
            {
                flight.Show();
            }
        }
    }
}