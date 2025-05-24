using System;

namespace UFF.Domain.Dto
{
    public class QueueReportDto
    {
        public string Name { get; set; }
        public DateTime QueueDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string TotalTime { get; set; }
    }
}