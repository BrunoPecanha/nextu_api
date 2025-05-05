using System.Collections.Generic;

namespace UFF.Domain.Dto
{
    public class StoreProfessionalsDto
    {
        public StoreProfessionalsDto(string name, string logoPath, string subtitle)
        {
            Name = name;
            StoreLogoPath = logoPath;
            Subtitle = subtitle;
        }

        public string StoreLogoPath { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public ICollection<ProfessionalDto> Professionals { get; set; } = new List<ProfessionalDto>();
    }
}