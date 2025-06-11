namespace UFF.Domain.Commands.Customer
{
    public class CustomerVariablesCommand
    {
        public int CustomerId { get; set; }
        public CustomerVariablesItemCommand[] CustomerServices { get; set; }
    }
}