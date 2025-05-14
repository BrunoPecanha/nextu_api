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
        public DateTimeOffset TimeEnteredQueue { get; private set; } = DateTime.UtcNow;
        public DateTimeOffset? TimeCalledInQueue { get; private set; }
        public DateTimeOffset? MissingCustomerRemovalTime { get; private set; }
        public DateTimeOffset? ServiceStartTime { get; private set; }
        public DateTimeOffset ? ServiceEndTime { get; private set; }
        public CustomerStatusEnum Status { get; private set; }
        public string RandomCustomerName { get; set; }
        public bool IsPriority { get; private set; }
        public string RemoveReason { get; set; }
        public virtual ICollection<CustomerService> CustomerServices { get; private set; } = new List<CustomerService>();

        public Customer(CustomerCreateCommand command)
        {
            QueueId = command.QueueId;
            UserId = command.UserId;
            Notes = command.Notes;
        }

        public Customer(User user, Queue queue, int paymentMethod, string notes, int position)
        {
            QueueId = queue.Id;            
            UserId = user.Id;  
            Notes = notes;
            Position = position;
            PaymentId = paymentMethod;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
        }

        public void RemoveMissingCustomer(string removeReason)
        {
            RemoveReason = removeReason;
            Status = CustomerStatusEnum.Removed;
            MissingCustomerRemovalTime = DateTime.UtcNow;
            Position = -1;
        }

        public void ExitQueue()
        {
            Status = CustomerStatusEnum.Canceled;
            Position = -1;
            RemoveReason = "Cliente saiu da fila";
        }


        public void SetTimeCalledInQueue()
        {
            TimeCalledInQueue = DateTime.UtcNow;
            Status = CustomerStatusEnum.Absent;
        }

        public void SetStartTime()
        {
            ServiceStartTime = DateTime.UtcNow;
            Status = CustomerStatusEnum.InService;
        }        

        public void SetEndTime()
        {
            ServiceStartTime = DateTime.UtcNow;
            Status = CustomerStatusEnum.Done;
            Position = -1;
        }

        public void AddReview(string review)
            => Review = review;

        public void AddRating(int rating)
            => Rating = rating;
    }
}