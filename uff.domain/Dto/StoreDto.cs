using System;
using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get;  set; }
        public string Address { get;  set; }
        public string Number { get; set; }
        public string City { get; set; }
        public int MinorQueue { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public decimal Rating { get; set; }
        public int Votes { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Site { get; set; }
        public string Whatsapp { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<OpeningHoursDto> OpeningHours { get; set; } = new List<OpeningHoursDto>();
        public virtual ICollection<HighLightDto> HighLights { get; set; } = new List<HighLightDto>();
    }
}