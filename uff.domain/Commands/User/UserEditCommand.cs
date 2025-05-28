using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace UFF.Domain.Commands.User
{
    public class UserEditCommand
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DDD { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string Email { get; set; }
        public string Subtitle { get; set; }
        public string ServicesProvided { get; set; }
        public bool AcceptAwaysMinorQueue { get; set; }
        public bool DeleteAccount { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordHashed { get; set; }
    }
}