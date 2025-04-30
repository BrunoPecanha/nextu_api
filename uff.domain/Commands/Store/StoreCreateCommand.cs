using System.Collections.Generic;

namespace UFF.Domain.Commands.Store
{
    public class StoreCreateCommand
    {
        public int OwnerId { get; set; }
        public string Cnpj { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public bool OpenAutomatic { get; private set; }
        public string StoreSubtitle { get; private set; }
        public bool AcceptOtherQueues { get; private set; }
        public bool AnswerOutOfOrder { get; private set; }
        public bool AnswerScheduledTime { get; private set; }
        public int? TimeRemoval { get; private set; }
        public bool WhatsAppNotice { get; private set; }
        public string LogoPath { get; set; }
        public string WallPaperPath { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<OpeningHoursCreateCommand> OpeningHours { get; private set; } = new List<OpeningHoursCreateCommand>();
        public virtual ICollection<HighLightCreateCommand> HighLights { get; private set; } = new List<HighLightCreateCommand>();

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Address) &&
                   !string.IsNullOrWhiteSpace(Number) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(State);
        }
    }
}