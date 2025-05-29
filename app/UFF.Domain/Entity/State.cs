namespace UFF.Domain.Entity
{
    public struct State
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }

        public State(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }
    }
}
