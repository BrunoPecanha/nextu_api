using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public ICollection<CustomerServiceDto> Services { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public string PaymentIcon { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
    }
}