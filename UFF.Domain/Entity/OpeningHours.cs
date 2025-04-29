using System;
using UFF.Domain.Commands.Store;

namespace UFF.Domain.Entity
{
    public class OpeningHours : To
    {
        private OpeningHours()
        {
        }
        public string WeekDay { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool Activated { get; set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }

        public OpeningHours(OpeningHoursCommand command)
        {
            WeekDay = command.WeekDay;
            Start = command.Start;
            End = command.End;
        }
    }
}
