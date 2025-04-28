using System.ComponentModel.DataAnnotations;
using UFF.Domain.Enum;

namespace UFF.Domain.Commands.Store
{
    public class StoreCreateCommand
    {
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cnpj { get; set; }        
        public int OwnerId { get; set; }
    }
}