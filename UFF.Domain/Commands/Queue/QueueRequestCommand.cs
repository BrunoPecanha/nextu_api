using System;

namespace UFF.Domain.Commands.Queue
{
    public class QueueRequestCommand
    {
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
    }
}