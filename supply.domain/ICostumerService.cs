using uff.Domain.Commands;
using uff.Domain.Entity;
using System.Threading.Tasks;

namespace uff.Domain
{
    public interface ICostumerService :  IServiceBase<Costumer> {
        public Task<CommandResult> CreateAsync(CostumerCommand command);
        public Task<CommandResult> UpdateAsync(CostumerCommand command);
        public Task<CommandResult> DeleteAsync(int id);
    }
}