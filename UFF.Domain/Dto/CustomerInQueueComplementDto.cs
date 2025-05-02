using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueComplementDto
    {
        public int? Id { get; set; }
        public ICollection<ServiceDto> Services { get; set; }
        public PaymentDto Payment { get; set; }
    }
}