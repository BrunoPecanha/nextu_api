using System.Text.Json.Serialization;

namespace uff.Domain.Commands.User
{
    public class UserCreateCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }   
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordHashed { get; set; }
    }
}