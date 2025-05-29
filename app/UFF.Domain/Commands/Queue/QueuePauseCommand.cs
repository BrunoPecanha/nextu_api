namespace UFF.Domain.Commands.Queue
{
    public class QueuePauseCommand
    {
        public int Id { get; set; }
        public string PauseReason { get; set; }
    }
}