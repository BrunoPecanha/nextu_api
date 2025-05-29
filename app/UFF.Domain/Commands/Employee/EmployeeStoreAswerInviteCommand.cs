
namespace UFF.Domain.Commands.Employee
{
    public class EmployeeStoreAswerInviteCommand
    {
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public bool Answer { get; set; }
    }
}