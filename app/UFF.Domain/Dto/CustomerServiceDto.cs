namespace UFF.Domain.Dto
{
    public class CustomerServiceDto
    {

        public CustomerServiceDto()
        {
        }

        public CustomerServiceDto(string name, string icon, int serviceId, int queueId, decimal total, int quantity)
        {
            ServiceId = serviceId;
            QueueId = queueId;          
            Price = total;
            Quantity = quantity;
            Name = name;
            Icon = icon;
        }

        public int ServiceId { get; set; }
        public int QueueId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}