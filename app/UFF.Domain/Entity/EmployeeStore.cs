namespace UFF.Domain.Entity
{
    public class EmployeeStore : To
    {
        private EmployeeStore()
        {
        }

        public EmployeeStore(int employeeId, int storeId)
        {
            EmployeeId = employeeId;
            StoreId = storeId;
            IsActive = false;
            RequestAnswered = false;
        }
        public int EmployeeId { get; private set; }
        public User Employee { get; private set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }
        public bool IsActive { get; private set; }
        public bool RequestAnswered { get; private set; }

        public void ActivateRelation()
        {
            IsActive = true;
        }

        public void InactivateRelation()
        {
            IsActive = false;
        }

        public void MarkAsAnswered()
        {
            RequestAnswered = true;
        }

        public void MarkAsNotAnswered()
        {
            RequestAnswered = false;
        }
    }
}
