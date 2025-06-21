using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueCardDto
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int CustomerId { get; set; }
        public TimeSpan TimeToWait { get; set; }
        public int ServiceQtd { get; set; }
        public CustomerStatusEnum Status { get; set; }
        public string StoreIcon { get; set; }
        public string Payment { get; set; }
        public string PaymentIcon { get; set; }
        public string LogoPath { get; set; }
        public int QueueId { get; set; }
        public int StoreId { get; set; }
        public bool IsPaused { get; set; }
    }
}