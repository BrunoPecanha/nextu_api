using System;

namespace UFF.Domain.Dto
{
    public class ServiceDto
    {
        public ServiceDto()
        {
        }

        public ServiceDto(string name, string icon, decimal price)
        {
            Name = name;
            Icon = icon;
            Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImgPath { get; set; }
        public bool Activated { get; set; }
        public bool VariableTime { get; set; }
        public bool VariablePrice { get; set; }
        public string Icon { get; set; }        
    }
}