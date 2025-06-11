using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class QueueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public QueueStatusEnum Status { get; set; }
        public int CurrentCount { get; set; }
        public int TotalCount { get; set; }
        public int ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public string QueueDescription { get; set; }
        public int QueueId { get; set; }
    }
}