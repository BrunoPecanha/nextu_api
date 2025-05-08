using System;

namespace UFF.Domain.Dto
{
    public class ProfessionalDto
    {
        public int QueueId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public bool Liked { get; set; }
        public string ServicesProvided { get; set; }
        public int CustomersWaiting { get; set; }
        public TimeSpan AverageWaitingTime { get; set; }
        public TimeSpan AverageServiceTime { get; set; }
    }
}