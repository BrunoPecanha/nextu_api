namespace UFF.Domain.Dto
{
    public class ServiceDto
    {
        public ServiceDto(string name, string icon, decimal price)
        {
            Name = name;
            Icon = icon;
            Price = price;
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
    }
}