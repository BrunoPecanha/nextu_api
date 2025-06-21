using System;
using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public bool Liked { get; set; }
        public string City { get; set; }
        public int MinorQueue { get; set; }
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public bool IsFavorite { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public double Rating { get; set; }
        public int Votes { get; set; }
        public string State { get; set; }
        public bool OpenAutomatic { get; set; }
        public string StoreSubtitle { get; set; }
        public bool AcceptOtherQueues { get; set; }
        public bool AnswerOutOfOrder { get; set; }
        public bool AnswerScheduledTime { get; set; }
        public int? TimeRemoval { get; set; }
        public bool WhatsAppNotice { get; set; }
        public bool AttendSimultaneously { get; set; }
        public string LogoPath { get; set; }
        public string WallPaperPath { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string WebSite { get; set; }
        public string Whatsapp { get; set; }
        public bool IsVerified { get; set; }
        public bool ReleaseOrdersBeforeGetsQueued { get; set; }
        public bool ShareQueue { get; set; }
        public bool StartServiceWithQRCode { get; set; }
        public bool EndServiceWithQRCode { get; set; }
        public bool InCaseFailureAcceptFinishWithoutQRCode { get; set;}
        public string PhoneNumber { get; set; }
        public virtual ICollection<OpeningHoursDto> OpeningHours { get; set; } = new List<OpeningHoursDto>();
        public virtual ICollection<HighLightDto> HighLights { get; set; } = new List<HighLightDto>();
    }
}