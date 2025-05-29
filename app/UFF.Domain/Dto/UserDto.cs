using Microsoft.AspNetCore.Http;
using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class UserDto {
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
        public ProfileEnum Profile { get; set; }
        public StatusEnum Status { get; set; }  
        public string Email { get;  set; }
        public string Subtitle { get; set; }
        public string ServicesProvided { get; set; }
        public string ImageUrl { get; set; }
        public bool AcceptAwaysMinorQueue { get; set; }
    }
}