namespace UFF.Domain.Entity
{
    public class EmployeeStore : To
    {
        private EmployeeStore()
        {
        }

        public EmployeeStore(int employeeId, int storeId, Store store)
        {
            EmployeeId = employeeId;
            StoreId = storeId;
            Store = store;
            IsActive = true;
        }

        public int EmployeeId { get; private set; }
        public User Employee { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }
        public bool? IsActive { get; private set; } = true;
    }
}
