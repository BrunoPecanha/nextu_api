using System;
using System.Collections.Generic;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueCardDetailsDto
    {
        public int Id { get; set; }
        public ICollection<CustomerServiceDto> Services { get; set; }
        public decimal Total { get; set; }
        public CustomerStatusEnum Status { get; set; }
        public int TotalPeopleInQueue { get; set; }
        public PaymentDto Payment { get; set; }
        public int Position { get; set; }
        public TimeSpan TimeToWait { get; set; }
        public string TimeCalledInQueue { get; set; }
        public int QueueId { get; set; }
        public string EstimatedWaitingTime { get; set; }
        public string AttendantsName { get; set; }
        public string Token { get; set; }
    }
}