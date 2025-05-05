using System;

namespace UFF.Domain.Dto
{
    public class CustomerInQueueCardDto
    {
        public int? Id { get; set; }
        public int Position { get; set; }
        public int ServiceQtd { get; set; }
        public string StoreIcon { get; set; }
        public string Payment { get; set; }
        public string PaymentIcon { get; set; }
        public string LogoPath { get; set; }
        public int QueueId { get; set; }
    }
}