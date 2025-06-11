namespace UFF.Domain.Dto
{
    public class CustomerServiceDto
    {

        public CustomerServiceDto()
        {
        }

        public int ServiceId { get; set; }
        public int QueueId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public int FinalDuration { get; set; }
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; }
        public bool VariablePrice { get; set; }
        public bool VariableTime { get; set; }
    }
}