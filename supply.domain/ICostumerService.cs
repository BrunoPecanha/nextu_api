using Supply.Domain.Commands;
using Supply.Domain.Entity;
using System.Threading.Tasks;

namespace Supply.Domain
{
    public interface ICostumerService :  IServiceBase<Costumer> {
        public Task<CommandResult> CreateAsync(CostumerCommand command);
        public Task<CommandResult> UpdateAsync(CostumerCommand command);
        public Task<CommandResult> DeleteAsync(int id);
    }
}