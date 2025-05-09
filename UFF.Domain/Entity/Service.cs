using System;

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
        public bool? Activated { get; private set; }

        public Service(string name)
        {
            Name = name;
            Activated = true;
        }
    }
}