using System;
using System.Collections.Generic;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public ICollection<CustomerServiceDto> Services { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentIcon { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public PriorityEnum Priority { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int ProcessedBy { get; set; }
        public string ProcessedByName { get; set; }
        public string RejectionReason { get; set; }
    }
}