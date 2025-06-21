using System.Collections.Generic;
using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }
        public ICollection<CustomerServiceDto> Items { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentIcon { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public PriorityEnum Priority { get; set; }
        public CustomerStatusEnum Status { get; set; }
        public DateTime? ProcessedAt { get; set; } 
        public string ProcessedByName { get; set; }
        public string RejectionReason { get; set; }
    }
}