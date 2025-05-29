using System;

namespace UFF.Domain.Dto
{
    public class OpeningHoursDto
    {
        public string WeekDay { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool Activated { get; set; }
    }
}