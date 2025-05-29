using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class StoreProfessionalsDto
    {
        public StoreProfessionalsDto(int id, string name, string logoPath, string subtitle, bool isVerified)
        {
            Name = name;
            StoreLogoPath = logoPath;
            Subtitle = subtitle;
            IsVerified = isVerified;
            Id = id;
        }

        public int Id { get; set; }
        public bool IsVerified { get; set;}
        public string StoreLogoPath { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public ICollection<ProfessionalDto> Professionals { get; set; } = new List<ProfessionalDto>();
    }
}