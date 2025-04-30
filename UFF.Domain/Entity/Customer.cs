using System.Collections.Generic;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Enum;

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
        public string? Notes { get; private set; }
        public int? Rating { get; private set; }
        public string? Review { get; private set; }
        public CustomerStatusEnum Status { get; set; }
        public virtual ICollection<QueueCustomer> QueueCustomers { get; private set; } = new List<QueueCustomer>();
        public virtual ICollection<CustomerService> CustomerServices { get; private set; } = new List<CustomerService>();

        public Customer(CustomerCreateCommand command)
        {
            QueueId = command.QueueId;
            UserId = command.UserId;
            Notes = command.Notes;
            Status = CustomerStatusEnum.Waiting;
        }

        public void AddReview(string review)
        {
            Review = review;
        }

        public void AddRating(int rating)
        {
            Rating = rating;
        }
    }
}