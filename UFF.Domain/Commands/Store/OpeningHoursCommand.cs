using System;

namespace UFF.Domain.Commands.Store
{
    public class OpeningHoursCommand
    {
        public string WeekDay { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool Activated { get; set; }
    }
}