using System;

namespace UFF.Domain.Commands.Queue
{
    public class CustomerHistoryFilterCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}