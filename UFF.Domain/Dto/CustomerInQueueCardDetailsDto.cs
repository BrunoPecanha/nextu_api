using System;
using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueCardDetailsDto
    {
        public int Id { get; set; }
        public ICollection<ServiceDto> Services { get; set; }
        public decimal Total { get; set; }
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