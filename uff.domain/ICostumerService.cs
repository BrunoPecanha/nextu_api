using System.Threading.Tasks;
using uff.Domain.Commands;

namespace uff.Domain
{
    public interface ICostumerService { 
        public Task<CommandResult> CreateAsync(CostumerCreateCommand command);
        public Task<CommandResult> UpdateAsync(CostumerEditCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}