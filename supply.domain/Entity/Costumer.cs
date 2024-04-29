using uff.Domain.Commands;

namespace uff.Domain.Entity
{
    public class Costumer : To {
        private Costumer()
        {
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }

        public Costumer(CostumerCommand costumer)
        {
            Name = costumer.Name;
            LastName = costumer.LastName;
            Phone = costumer.Phone;
            Street= costumer.Street;
            Number = costumer.Number;
            City= costumer.City;
            Active = true;
        }

        public void UpdateAllInfo(CostumerCommand costumer)
        {
            Name = costumer.Name;
            LastName = costumer.LastName;
            Phone = costumer.Phone;
            Street = costumer.Street;
            Number = costumer.Number;
            City = costumer.City;
            Active = costumer.Active;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Street)
                || string.IsNullOrEmpty(Number) || string.IsNullOrEmpty(City));
        }
    }
}
