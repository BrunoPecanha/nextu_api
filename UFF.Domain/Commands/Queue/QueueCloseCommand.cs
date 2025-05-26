namespace UFF.Domain.Commands.Queue
{
    public class QueueCloseCommand
    {
        public int Id { get; set; }
        public string CloseReason { get; set; }
    }
}