namespace UFF.Domain.Commands.Queue
{
    public class QueueAddCustomerCommand
    {
        public QueueServiceCommand[] SelectedServices { get; set; }
        public string Notes { get; set; }
        public int PaymentMethod { get; set; }
        public int QueueId { get; set; }
        public int UserId { get; set; }
    }
}