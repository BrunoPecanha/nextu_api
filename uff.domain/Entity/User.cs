using System;
using uff.domain.Commands.User;
using uff.Domain.Enum;


namespace uff.Domain.Entity
{
    public class User : To {
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
            Password = user.PasswordHashed;
            LastName = user.LastName;
            Phone = user.Phone;
            Street= user.Street;
            Number = user.Number;
            City= user.City;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            Profile = ProfileEnum.User;
        }

        public void UpdateAllInfo(UserEditCommand user)
        {
            Name = user.Name;
            LastName = user.LastName;
            Password = Password = user.Password; 
            Phone = user.Phone;
            Street = user.Street;
            Number = user.Number;
            City = user.City;
            Status = user.Status;
            LastUpdate = DateTime.UtcNow;
            Profile = user.Profile == ProfileEnum.Admin ? ProfileEnum.User : user.Profile;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Street)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(Number) || string.IsNullOrEmpty(City));
        }
    }
}
