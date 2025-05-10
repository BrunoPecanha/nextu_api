using System;
using UFF.Domain.Commands.Service;

namespace UFF.Domain.Entity
{
    public class Service : To
    {
        private Service()
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ServiceCategory Category { get; set; }
        public int CategoryId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string ImgPath { get; private set; }
        public bool VariableTime { get; private set; }
        public bool VariablePrice { get; private set; }
        public bool Activated { get; private set; }

        public Service(ServiceCreateCommand command, int categoryId, int storeId, string imgPath)
        {
            Name = command.Name;
            Description = command.Description;
            CategoryId = categoryId;
            StoreId = storeId;
            Activated = command.Activated;
            Price = command.Price;
            Duration = command.Duration;
            VariableTime = command.VariableTime;
            VariablePrice = command.VariablePrice;
            ImgPath= imgPath;
        }

        public void UpdateServiceDetails(ServiceEditCommand command, string imgPath)
        {
            Name = command.Name;
            Description = command.Description;
            CategoryId = command.CategoryId;
            StoreId = command.StoreId;
            Activated = command.Activated;
            Price = command.Price;
            Duration = command.Duration;
            VariableTime = command.VariableTime;
            VariablePrice = command.VariablePrice;
            ImgPath = imgPath;
        }
    }
}