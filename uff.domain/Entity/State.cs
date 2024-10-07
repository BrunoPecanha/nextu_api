namespace uff.Domain.Entity
{
    public struct State
    {
        public string Name { get; set; }
        public string Acronym { get; set; }

        public State(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }
    }
}
