// See https://aka.ms/new-console-template for more information
using FlightCalendar;

var schedule = new Schedule(DateTime.Now);
var flight1 = new Flight("9U 737", new TimeSpan(10, 15, 0), new TimeSpan(0, 15, 0));
var flight2 = new Flight("9U 877", new TimeSpan(13, 0, 0), new TimeSpan(0, 15, 0));
var flight3 = new Flight("10U 877", new TimeSpan(14, 0, 0), new TimeSpan(0, 15, 0));

string error = "";
schedule.TryAddFlight(flight1, out error);
schedule.TryAddFlight(flight2, out error);
schedule.TryAddFlight(flight3, out error);

var plan = schedule.GetScheduledFlightsDescription();
foreach (var row in plan)
{
    Console.WriteLine(row);
}
Console.WriteLine(error);
Console.ReadKey();
