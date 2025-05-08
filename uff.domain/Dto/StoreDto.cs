using System;
using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Cnpj { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public int MinorQueue { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public decimal Rating { get; set; }
        public int Votes { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<OpeningHoursDto> OpeningHours { get; private set; } = new List<OpeningHoursDto>();
        public virtual ICollection<HighLightDto> HighLights { get; private set; } = new List<HighLightDto>();
    }
}