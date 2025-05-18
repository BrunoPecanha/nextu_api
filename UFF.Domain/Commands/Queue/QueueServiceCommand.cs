namespace UFF.Domain.Commands.Queue
{
    public class QueueServiceCommand
    {
        public int ServiceId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}