using System.Threading.Tasks;
using uff.Domain.Commands;

namespace uff.Domain
{
    public interface ICostumerService { 
        public Task<CommandResult> CreateAsync(CostumerCommand command);
        public Task<CommandResult> UpdateAsync(CostumerCommand command);
        public Task<CommandResult> DeleteAsync(int id);
    }
}