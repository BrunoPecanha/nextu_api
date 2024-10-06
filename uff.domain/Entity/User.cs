using System;
using uff.domain.Commands.User;
using uff.Domain.Enum;

namespace uff.Domain.Entity
{
    public class User : To
    {
        private User()
        {
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public StatusEnum Status { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ProfileEnum Profile { get; private set; }

        public User(UserCreateCommand user)
        {
            Name = user.Name;
            Email = user.Email;
            LastName = user.LastName;
            Phone = user.Phone;
            Street = user.Street;
            Password = user.Password;
            Number = user.Number;
            City = user.City;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            Profile = ProfileEnum.User;
        }

        public void UpdateAllInfo(UserEditCommand user)
        {
            Name = !string.IsNullOrWhiteSpace(user.Name) ? user.Name : this.Name;
            LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : this.LastName;            
            Phone = !string.IsNullOrWhiteSpace(user.Phone) ? user.Phone : this.Phone;
            Street = !string.IsNullOrWhiteSpace(user.Street) ? user.Street : this.Street;
            Number = !string.IsNullOrWhiteSpace(user.Number) ? user.Number : this.Number;
            City = !string.IsNullOrWhiteSpace(user.City) ? user.City : this.City;
            Status = user.Status;
            LastUpdate = DateTime.UtcNow; 
            
            CheckProfileChange(user.Profile);
        }

        public void UpdatePassWord(string passWord)
            => Password = passWord;

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Street)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(Number) || string.IsNullOrEmpty(City));
        }

        private void CheckProfileChange(ProfileEnum newProfile)
        {
            if (newProfile != ProfileEnum.Admin)
                this.Profile = newProfile;
        }
    }
}
