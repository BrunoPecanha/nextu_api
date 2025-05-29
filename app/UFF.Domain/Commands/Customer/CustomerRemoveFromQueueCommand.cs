namespace UFF.Domain.Commands.Customer
{
    public class CustomerRemoveFromQueueCommand
    {        
        public int CustomerId { get; set; }
        public string RemoveReason { get; set; }
    }
}