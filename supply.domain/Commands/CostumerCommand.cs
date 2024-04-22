namespace Supply.Domain.Commands
{
    public class CostumerCommand {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }    
        public bool Active { get; set; }
    }
}