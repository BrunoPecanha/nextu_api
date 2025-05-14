namespace UFF.Domain.Dto
{
    public class CustomerInQueueForEmployeeDto
    {
        public int? Id { get; set; }
        public string Payment { get; set; }
        public string PaymentIcon { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public int QueueId { get; set; }
        public string TimeGotInQueue { get; set; }
        public string TimeCalledInQueue { get; set; }        
        public bool InService { get; set; }
    }
}