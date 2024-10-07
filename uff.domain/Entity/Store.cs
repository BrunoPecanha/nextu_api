using System;
using uff.Domain.Commands.Store;
using uff.Domain.Enum;

namespace uff.Domain.Entity
{
    public class Store : To
    {
        private Store()
        {
        }
        public string Description { get; private set; }        
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public int OwnerId { get; private set; }
        public User Owner { get; private set; }
        public string Cnpj { get; set; }
        public StatusEnum Status { get; private set; }   

        public Store(StoreCreateCommand store)
        {
            Description = store.Description;                    
            Phone = store.Phone;
            Address = store.Address;
            Number = store.Number;
            City = store.City;
            Cnpj= store.Cnpj;
            State = store.State;
            Status = StatusEnum.Enabled;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
        }

        public void UpdateAllUserInfo(StoreEditCommand user)
        {
            Description = !string.IsNullOrWhiteSpace(user.Description) ? user.Description : this.Description;            
            Phone = !string.IsNullOrWhiteSpace(user.Phone) ? user.Phone : this.Phone;
            Address = !string.IsNullOrWhiteSpace(user.Address) ? user.Address : this.Address;
            Number = !string.IsNullOrWhiteSpace(user.Number) ? user.Number : this.Number;
            City = !string.IsNullOrWhiteSpace(user.City) ? user.City : this.City;
            Status = user.Status;
            LastUpdate = DateTime.UtcNow; 
        }
      

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Description)
                || string.IsNullOrEmpty(Phone) 
                || string.IsNullOrEmpty(Address)
                || string.IsNullOrEmpty(Number)
                || string.IsNullOrEmpty(City));
        }


        public void SetOwner(User owner)
        {
            OwnerId = owner.Id;
        }

        public void Disable()
         => Status = StatusEnum.Disabled;

        public void Enable()
            => Status = StatusEnum.Enabled;

        private void UpdateCnpj(string cnpj)
        {
            if (!string.IsNullOrWhiteSpace(cnpj) && cnpj.Length == 14)
                this.Cnpj = cnpj;
        }    
    }
}
