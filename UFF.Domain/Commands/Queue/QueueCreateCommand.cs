namespace UFF.Domain.Commands.Queue
{
    public class QueueCreateCommand
    {
        public int StoreId { get; set; }
        public string Description { get; set; }
    }
}