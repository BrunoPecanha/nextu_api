using System;
using System.Linq;
using System.Threading.Tasks;
using uff.Domain.Commands;
using uff.Domain.Entity;
using uff.Service.Properties;

namespace uff.Service
{
    public static class StateService
    {
        private static readonly State[] _states = new State[]
        {
            new State("Acre", "AC"),
            new State("Alagoas", "AL"),
            new State("Amapá", "AP"),
            new State("Amazonas", "AM"),
            new State("Bahia", "BA"),
            new State("Ceará", "CE"),
            new State("Distrito Federal", "DF"),
            new State("Espírito Santo", "ES"),
            new State("Goiás", "GO"),
            new State("Maranhão", "MA"),
            new State("Mato Grosso", "MT"),
            new State("Mato Grosso do Sul", "MS"),
            new State("Minas Gerais", "MG"),
            new State("Pará", "PA"),
            new State("Paraíba", "PB"),
            new State("Paraná", "PR"),
            new State("Pernambuco", "PE"),
            new State("Piauí", "PI"),
            new State("Rio de Janeiro", "RJ"),
            new State("Rio Grande do Norte", "RN"),
            new State("Rio Grande do Sul", "RS"),
            new State("Rondônia", "RO"),
            new State("Roraima", "RR"),
            new State("Santa Catarina", "SC"),
            new State("São Paulo", "SP"),
            new State("Sergipe", "SE"),
            new State("Tocantins", "TO")
        };

        public static async Task<CommandResult> GetAllAsync()
            => await Task.FromResult(new CommandResult(true,  _states));

        public static Task<CommandResult> GetBySiglaAsync(string acronym)
        {
            State? state = _states.FirstOrDefault(s => s.Acronym.Equals(acronym, StringComparison.OrdinalIgnoreCase));

            if (state.Value.Name is null)
                return Task.FromResult(new CommandResult(false, Resources.StateNofFound));

            return Task.FromResult(new CommandResult(true, state));
        }
    }
}
