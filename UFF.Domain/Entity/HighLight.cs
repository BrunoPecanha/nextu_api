using UFF.Domain.Commands.Store;

namespace UFF.Domain.Entity
{
    public class HighLight : To
    {
        private HighLight()
        {
        }

        public string Phrase { get; private set; }
        public string Icon { get; private set; }
        public bool Activated { get; set; }
        public int StoreId { get; private set; }
        public Store Store { get; private set; }

        public HighLight(HighLightCreateCommand command)
        {
            Phrase = command.Frase;
            Icon = command.Icone;
            Activated = true;
        }
    }
}