using System.Collections.Generic;

namespace UFF.Domain.Commands.Store
{
    public class StoreCreateCommand
    {
        public int OwnerId { get; set; }
        public string Cnpj { get;  set; }
        public string Name { get;  set; }
        public string Address { get;  set; }
        public string Number { get;  set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool OpenAutomatic { get; set; }
        public string StoreSubtitle { get; set; }
        public bool AcceptOtherQueues { get; set; }
        public bool AnswerOutOfOrder { get; set; }
        public bool AnswerScheduledTime { get; set; }
        public int? TimeRemoval { get; set; }
        public bool WhatsAppNotice { get; set; }
        public string LogoPath { get; set; }
        public string WallPaperPath { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<OpeningHoursCreateCommand> OpeningHours { get; set; } = new List<OpeningHoursCreateCommand>();
        public virtual ICollection<HighLightCreateCommand> HighLights { get; set; } = new List<HighLightCreateCommand>();      
    }
}