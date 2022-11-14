using System;

namespace FlightCalendar
{
    //FlightNumber should not be null
    //Start time should not be earlier than 3 AM and later than 23.45PM
    //Take-off time should be max 15 minutes
    public class Flight
    {

        private string bortNumber;
        private TimeSpan start;
        private TimeSpan duration;

        public Flight(string topic, TimeSpan startTime, TimeSpan duration)
        {
            SetFlightNumber(topic);
            SetStartTime(startTime);
            SetDuration(duration);
        }

        public string BortNumber
        {
            get => this.bortNumber;
            set => SetFlightNumber(value);
        }

        public TimeSpan Start
        {
            get => this.start;
            set => SetStartTime(value);
        }

        public TimeSpan Duration
        {
            set => SetDuration(value);
        }

        public TimeSpan End => this.start + this.duration;


        internal void Show()
        {
            Console.WriteLine($"{BortNumber}, {Start} - {End};");
        }
        private void SetFlightNumber(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException($"Flight should have a number");
            }
            this.bortNumber = topic;
        }
        private void SetDuration(TimeSpan duration)
        {
            if ((duration < new TimeSpan(0, 5, 0)) || (duration > new TimeSpan(0, 15, 0)))
            {
                throw new System.ArgumentException($"Invalid take-off duration", "duration");
            }
            this.duration = duration;
        }

        private void SetStartTime(TimeSpan value)
        {
            if (!TimeService.IsWithinWorkDay(value))
            {
                throw new System.ArgumentException($"Flight cannot start at {value}");
            }
            start = value;
        }

        public bool Overlaps(Flight flight)
        {
            return (flight.Start >= this.Start && flight.start < this.End) ||
                (flight.End > this.Start && flight.End <= this.End) ||
                (flight.Start <= this.Start && flight.End >= this.End);
        }
        public string GetDescription()
        {
            return $"{this.bortNumber}, {this.start} - {this.start + this.duration};";
        }
    }
}