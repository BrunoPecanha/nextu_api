using System;
using UFF.Domain.Enum;
using System.Collections.Generic;
using UFF.Domain.Commands.Customer;

namespace UFF.Domain.Entity
{
    public class Customer : To
    {
        private Customer()
        {
        }

        public Queue Queue { get; private set; }
        public int QueueId { get; private set; }
        public User User { get; private set; }
        public int UserId { get; private set; }
        public Payment Payment { get; private set; }
        public int PaymentId { get; private set; }
        public string? Notes { get; private set; }
        public int? Rating { get; private set; }
        public string? Review { get; private set; }
        public int Position { get; private set; }
        public DateTime TimeEnteredQueue { get; private set; } = DateTime.UtcNow;
        public DateTime? TimeCalledInQueue { get; private set; }
        public DateTime? ServiceStartTime { get; private set; }
        public DateTime? ServiceEndTime { get; private set; }
        public CustomerStatusEnum Status { get; private set; }
        public bool IsPriority { get; private set; }
        public virtual ICollection<QueueCustomer> QueueCustomers { get; private set; } = new List<QueueCustomer>();
        public virtual ICollection<CustomerService> CustomerServices { get; private set; } = new List<CustomerService>();

        public Customer(CustomerCreateCommand command)
        {
            QueueId = command.QueueId;
            UserId = command.UserId;
            Notes = command.Notes;
        }

        public void SetTimeCalledInQueue()
          => TimeCalledInQueue = DateTime.UtcNow;

        public void SetStartTime()
            => ServiceStartTime = DateTime.UtcNow;

        public void SetEndTime()
           => ServiceEndTime = DateTime.UtcNow;

        public void AddReview(string review)
            => Review = review;        

        public void AddRating(int rating)
            => Rating = rating;        
    }
}