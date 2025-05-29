using System;
using UFF.Domain.Commands.Store;

namespace UFF.Domain.Entity
{
    public class OpeningHours : To
    {
        private OpeningHours()
        {
        }
        public string WeekDay { get; private set; }
        public TimeSpan? Start { get; private set; }
        public TimeSpan? End { get; private set; }
        public bool Activated { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }

        public OpeningHours(OpeningHoursCreateCommand command)
        {
            WeekDay = command.WeekDay;
            Start = command.Start;
            End = command.End;
        }

        public void SetStart(TimeSpan? start)
        {
            if (start != null) 
                Start = start;
        }

        public void SetEnd(TimeSpan? end)
        {
            if (end != null)
                End = end;
        }
    }
}
