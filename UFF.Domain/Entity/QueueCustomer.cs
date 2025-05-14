using System;

namespace UFF.Domain.Entity
{
    public class QueueCustomer : To
    {
        private QueueCustomer()
        {
        }

        public QueueCustomer(Customer customer, User user, Queue queue)
        {
            QueueId = queue.Id;
            Customer = customer;
            CustomerId = customer.Id;    
            UserId = user.Id;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
        }

        public int QueueId { get; private set; }
        public Queue Queue { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
    }
}