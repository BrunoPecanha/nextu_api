using System;
using System.Collections.Generic;
using UFF.Domain.Enum;

namespace UFF.Domain.Entity
{
    public class Queue : To
    {
        private Queue()
        {
        }

        public Store Store { get; private set; }
        public int StoreId { get; private set; }
        public User Employee { get; private set; }
        public int EmployeeId { get; private set; }

        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime? ClosingDate { get; private set; }
        public QueueStatusEnum Status { get; private set; }
        public string PauseReason { get; private set; }
        public virtual ICollection<Customer> Customers { get; private set; } = new List<Customer>();

        public Queue(string description, int storeId, int employeeId)
        {
            Name = description;
            Date = DateTime.UtcNow;
            EmployeeId = employeeId;
            StoreId = storeId;
            Status = QueueStatusEnum.Open;
        }

        public void Close()
        {
            ClosingDate = DateTime.UtcNow;
            Status= QueueStatusEnum.Closed;
        }

        public void Pause(string pauseReason)
        {
            PauseReason = pauseReason;
            Status = QueueStatusEnum.Paused;
        }

        public void Unpause()
        {
            Status = QueueStatusEnum.Open;
        }

        public bool IsValid()
            => !string.IsNullOrEmpty(Name);
    }
}
