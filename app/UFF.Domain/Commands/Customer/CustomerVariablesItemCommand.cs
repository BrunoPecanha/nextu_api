namespace UFF.Domain.Commands.Customer
{
    public class CustomerVariablesItemCommand
    {
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}