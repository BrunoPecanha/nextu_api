using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;

namespace UFF.Domain.Services
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