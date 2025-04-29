using System.Collections.Generic;

namespace UFF.Domain.Commands.Store
{
    public class StoreRegistrationCommand
    {
        public string Cnpj { get; set; }
        public string NomeEmpresa { get; set; }
        public string Endereco { get; set; }
        public List<OpeningHoursCommand> Horarios { get; set; } = new List<OpeningHoursCommand>();
        public bool AbrirAutomaticamente { get; set; }
        public bool AceitarOutrasFilas { get; set; }
        public bool AtenderForaDeOrdem { get; set; }
        public bool AtenderHoraMarcada { get; set; }
        public int? TempoRemocao { get; set; }
        public bool AvisoWhatsApp { get; set; }
        public string SubtituloLoja { get; set; }
        public List<HighLightCommand> Destaques { get; set; } = new List<HighLightCommand>();
    }
}