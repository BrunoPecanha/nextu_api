using System;
using UFF.Domain.Commands.Customer;

namespace UFF.Domain.Entity
{
    public class CustomerService : To
    {
        private CustomerService()
        {
        }
        public CustomerService(CustomerServiceCreateCommand command)
        {
            CustomerId = command.CustomerId;
            ServiceId = command.ServiceId;
            QueueId = command.QueueId;
            FinalPrice = command.FinalPrice;
        }

        public CustomerService(Customer customer, Service service, Queue queue, int quantity, decimal price, TimeSpan duration)
        {
            Quantity = quantity;
            CustomerId = customer.Id;
            Customer = customer;
            ServiceId = service.Id;
            QueueId = queue.Id;
            FinalPrice = quantity * price;
            Duration = quantity * duration;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetQueuId(int queueId)
        {
            QueueId= queueId;
        }

        public void SetFinalPrice(decimal finalPrice)
        {
            FinalPrice = finalPrice;
        }

        public int Quantity { get; set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public int ServiceId { get; private set; }
        public Service Service { get; private set; }

        public decimal FinalPrice { get; private set; }
        public TimeSpan Duration { get; private set; }

        public int QueueId { get; private set; }
        public Queue Queue { get; private set; }
    }
}