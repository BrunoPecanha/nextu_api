using System;
using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueForEmployeeDto
    {
        public int? Id { get; set; }
        public string Payment { get; set; }
        public string PaymentIcon { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerServiceDto> Services { get; set; }
        public int QueueId { get; set; }
        public string TimeGotInQueue { get; set; }
        public string TimeCalledInQueue { get; set; }        
        public bool InService { get; set; }
        public bool IsPaused { get; set; }
        public bool CanEditName { get; set; }
        public bool PricePending { get; set; }
    }
}