using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Commands.Queue
{
    public class QueueFilterRequestCommand
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ResponsibleId { get; set; }
        public QueueStatusEnum? QueueStatus { get; set; } 
    }
}