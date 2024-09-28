using System.Text.Json.Serialization;
using uff.Domain.Enum;

namespace uff.domain.Commands.User
{
    public class UserCreateCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordHashed { get; set; }
    }
}