using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueForStoreDto
    {
        public int QueueId { get; set; }
        public ICollection<CustomerInQueueForEmployeeDto> CustomerInQueueForEmployee { get; set; }
    }
}