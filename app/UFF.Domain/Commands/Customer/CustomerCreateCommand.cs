namespace UFF.Domain.Commands.Customer
{
    public class CustomerCreateCommand
    {
        public int QueueId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
    }
}