using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Entity
{
    public class QueueCustomer : To
    {
        private QueueCustomer()
        {
        }

        public int QueueId { get; private set; }
        public Queue Queue { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }    
        public int Position { get; private set; }
        public DateTime TimeEnteredQueue { get; private set; } = DateTime.UtcNow;
        public DateTime TimeCalledInQueue { get; private set; }
        public DateTime ServiceStartTime { get; private set; }
        public DateTime ServiceEndTime { get; private set; }
        public CustomerStatusEnum Status { get; private set; }
        public bool IsPriority { get; private set; }

        public void SetTimeCalledInQueue()
          => TimeCalledInQueue = DateTime.UtcNow;

        public void SetStartTime()
            => ServiceStartTime = DateTime.UtcNow;

        public void SetEndTime()
           => ServiceEndTime = DateTime.UtcNow;
    }
}