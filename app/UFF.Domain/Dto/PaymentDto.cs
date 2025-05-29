namespace UFF.Domain.Dto
{
    public class PaymentDto
    {
        public PaymentDto(string name, string icon, string details)
        {
            Name = name;
            Icon = icon;
            Details = details;
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Details { get; set; }
    }
}