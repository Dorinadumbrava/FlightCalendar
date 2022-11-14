// See https://aka.ms/new-console-template for more information
using FlightCalendar;

var schedule = new Schedule(DateTime.Now);
var flight1 = new Flight("9U 737", new TimeSpan(10, 15, 0), new TimeSpan(0, 15, 0));
var flight2 = new Flight("9U 877", new TimeSpan(13, 0, 0), new TimeSpan(0, 15, 0));
var flight3 = new Flight("10U 877", new TimeSpan(14, 0, 0), new TimeSpan(0, 15, 0));

List<Result> results = new List<Result>();
schedule.AddFlight(flight1);
schedule.AddFlight(flight2);
schedule.AddFlight(flight3);

var plan = schedule.GetScheduledFlightsDescription();
foreach (var row in plan)
{
    Console.WriteLine(row);
}
foreach (var item in results)
{
	if (!item.IsSuccess)
	{
		Console.WriteLine(item.Error);
	}
}
Console.ReadKey();
