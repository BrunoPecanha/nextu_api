namespace UFF.Domain.Commands.Queue
{
    public class CustomerHistoryDto
    {
        public string EstablishmentName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Services { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
    }
}
