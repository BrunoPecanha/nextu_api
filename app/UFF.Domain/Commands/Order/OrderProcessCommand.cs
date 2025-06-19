using UFF.Domain.Enum;

namespace UFF.Domain.Commands.Order
{
    public class OrderProcessCommand
    {
        public CustomerStatusEnum Status { get; set; }
        public string RejectReason { get; set; }
        public int ResposibleEmployee { get; set; }
    }
}