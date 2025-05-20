namespace UFF.Domain.Commands.Customer
{
    public class CustomerServiceCreateCommand
    {        
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int QueueId { get; set; }
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; }
    }
}