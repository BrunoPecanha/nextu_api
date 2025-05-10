namespace UFF.Domain.Entity
{
    public class EmployeeStore : To
    {
        public int EmployeeId { get; private set; }
        public User Employee { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }
        public bool IsActive { get; private set; }
    }
}
