using System;

namespace UFF.Domain.Commands.Store
{
    public class OpeningHoursCreateCommand
    {

        public OpeningHoursCreateCommand(string weekDay, TimeSpan? start, TimeSpan? end, bool activated)
        {
            WeekDay = weekDay;
            Start = start;
            End = end;
            Activated = activated;
        }

        public string WeekDay { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool Activated { get; set; }
    }
}