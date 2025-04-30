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
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public QueueStatusEnum Status { get; private set; }
        public virtual ICollection<QueueCustomer> QueueCustomers { get; private set; } = new List<QueueCustomer>();

        public Queue(string description, Store store)
        {
            Name = description;
            Date = DateTime.UtcNow;
            Store = store;
            StoreId = Store.Id;
        }

        public bool IsValid()
            => !string.IsNullOrEmpty(Name);
    }
}
