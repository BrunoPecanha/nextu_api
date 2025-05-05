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
        public Task<CommandResult> GetByCategoryIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
        public Task<CommandResult> GetByOwnerIdAsync(int id);
        public Task<CommandResult> GetByEmployeeId(int id);
        public Task<CommandResult> GetStoreWithProfessionalsAndWaitInfoAsync(int storeId);
        public Task LikeProfessional(LikeStoreProfessionalCommand command);
    }
}