using System.Threading.Tasks;
using uff.Domain.Commands;
using uff.Domain.Commands.Store;

namespace uff.domain.Services
{
    public interface IStoreService
    {
        public Task<CommandResult> CreateAsync(StoreCreateCommand command);
        public Task<CommandResult> UpdateAsync(StoreEditCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}