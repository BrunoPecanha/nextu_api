using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class StoreDto {
        public int Id { get; set; }
        public string Description { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public int OwnerId { get; private set; }
        public UserDto Owner { get; private set; }
        public string Cnpj { get; set; }
        public StatusEnum Status { get; private set; }
    }
}