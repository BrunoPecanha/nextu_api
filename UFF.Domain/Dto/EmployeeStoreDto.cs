using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class EmployeeStoreDto
    {
        public ICollection<EmployeeStoreItemDto> EmployeeStoreAssociations { get; set; }
    }
}