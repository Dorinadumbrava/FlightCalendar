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

        public Result AddFlight(Flight flight)
        {
            if (flight is null)
            {
                throw new
                    ArgumentNullException("Can not add flight to schedule: flight is null", "flight");
            }
            if (Flights.Any(x => flight.Overlaps(x)))
            {
                return Result.Failed($"Overlaping flight: {flight.BortNumber}");
            }
            Flights.Add(flight);
            return Result.Success();
        }

        public List<string> GetScheduledFlightsDescription()
        {
            var flightDescriptions = new List<string>();
            foreach (var flight in this.Flights)
            {
                flightDescriptions.Add(flight.GetDescription());
            }
            return flightDescriptions;
        }
    }
}