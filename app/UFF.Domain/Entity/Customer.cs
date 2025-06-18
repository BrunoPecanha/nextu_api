using System;
using System.Collections.Generic;
using System.Linq;
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
        public Payment Payment { get; private set; }
        public int PaymentId { get; private set; }
        public string? Notes { get; private set; }
        public int? Rating { get; private set; }
        public string Review { get; private set; }
        public int Position { get; private set; }
        public TimeSpan EstimatedWaitingTime { get; set; }
        public DateTime TimeEnteredQueue { get; private set; } = DateTime.UtcNow;
        public DateTime? TimeCalledInQueue { get; private set; }
        public DateTime? MissingCustomerRemovalTime { get; private set; }
        public DateTime? ServiceStartTime { get; private set; }
        public DateTime? ServiceEndTime { get; private set; }
        public CustomerStatusEnum Status { get; private set; }
        public string RandomCustomerName { get; set; }
        public bool IsPriority { get; private set; }
        public string RemoveReason { get; set; }
        public virtual ICollection<CustomerService> Items { get; private set; } = new List<CustomerService>();
        public PriorityEnum Priority { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int? ProcessedById { get; set; }
        public User ProcessedBy { get; set; }
        public string RejectionReason { get; set; }

        public Customer(CustomerCreateCommand command)
        {
            QueueId = command.QueueId;
            UserId = command.UserId;
            Notes = command.Notes;
        }

        public Customer(User user, Queue queue, int paymentMethod, string notes, int position, bool looseCustomer, bool releaseOrdeBeforeQueued)
        {
            QueueId = queue.Id;
            UserId = user.Id;
            Notes = notes;
            Position = position;
            PaymentId = paymentMethod;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            RandomCustomerName = looseCustomer ? user.Name : null;
            Status = releaseOrdeBeforeQueued ? CustomerStatusEnum.Pending : CustomerStatusEnum.Waiting;
        }

        public void UpdateCustomer(CustomerEditServicesPaymentCommand command, int queueId)
        {
            var existingServices = Items.ToList();

            foreach (var existing in existingServices)
            {
                if (!command.SelectedServices.Any(s => s.ServiceId == existing.ServiceId))
                {
                    Items.Remove(existing);
                }
            }

            foreach (var service in command.SelectedServices)
            {
                var existing = Items.FirstOrDefault(cs => cs.ServiceId == service.ServiceId);
                if (existing != null)
                {
                    existing.SetFinalPrice(service.Quantity * service.Price);
                    existing.SetQueuId(queueId);
                    existing.SetQuantity(service.Quantity);
                }
                else
                {
                    Items.Add(new CustomerService(new CustomerServiceCreateCommand()
                    {
                        CustomerId = Id,
                        FinalPrice = service.Quantity * service.Price,
                        QueueId = queueId,
                        ServiceId = service.ServiceId,
                        Quantity = service.Quantity
                        
                    }));
                }
            }

            PaymentId = command.PaymentMethod;
            Notes = command.Notes;
        }

        public void RemoveMissingCustomer(string removeReason)
        {
            RemoveReason = removeReason;
            Status = CustomerStatusEnum.Removed;
            MissingCustomerRemovalTime = DateTime.UtcNow;
        }

        public void SetRejectInfo(string rejectReason, int UserResponsibleForRemoval)
        {
            RejectionReason = rejectReason;
            Status = CustomerStatusEnum.Rejected;
            ProcessedAt = DateTime.UtcNow;
            ProcessedById = UserResponsibleForRemoval;
        }

        public void ExitQueue()
        {
            Status = CustomerStatusEnum.Canceled;
            RemoveReason = "Cliente saiu da fila";
        }

        public void SetPosition(int position)
            => Position = position;

        public void SetTimeCalledInQueue()
        {
            TimeCalledInQueue = DateTime.UtcNow;
            Status = CustomerStatusEnum.Absent;
            Position = 0;
        }
        public void SetStartTime()
        {
            ServiceStartTime = DateTime.UtcNow;
            Status = CustomerStatusEnum.InService;
        }

        public void UpdateName(string name)
        {
           RandomCustomerName = name;
        }

        public void SetEndTime()
        {
            ServiceEndTime = DateTime.UtcNow;
            Status = CustomerStatusEnum.Done;
        }

        public void AddReview(string review)
            => Review = review;

        public void AddRating(int rating)
            => Rating = rating;
    }
}