using uff.Domain.Enum;

namespace uff.Domain.Commands.User
{
    public class UserEditCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Cpf { get; set; }
        public StatusEnum Status { get; set; }
        public ProfileEnum Profile { get; set; }
        public string Password { get; set; }
    }
}