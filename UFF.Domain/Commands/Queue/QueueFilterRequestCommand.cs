using System;

namespace UFF.Domain.Commands.Queue
{
    public class QueueFilterRequestCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}