using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;

namespace UFF.Domain.Services
{
    public interface IStoreRatingService
    {
        public Task<CommandResult> CreateAsync(StoreRatingCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> GetByStoreIdAsync(int id);
    }
}