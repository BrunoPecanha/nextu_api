using System;

namespace UFF.Domain.Dto
{
    public class EmployeeStoreItemDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string AssociatedSince { get; set; }
        public bool InviteIsPending { get; set; }
    }
}