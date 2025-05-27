using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;
using UFF.Domain.Enum;

namespace UFF.Domain.Services
{
    public interface IStoreService
    {
        public Task<CommandResult> CreateAsync(StoreCreateCommand command);
        public Task<CommandResult> UpdateAsync(StoreEditCommand command, int storeId);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> GetByCategoryIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
        public Task<CommandResult> GetByOwnerIdAsync(int id);
        public Task<CommandResult> GetByEmployeeId(int id, ProfileEnum profile);
        public Task<CommandResult> GetStoreWithProfessionalsAndWaitInfoAsync(int storeId);
        public Task<CommandResult> GetProfessionalsOfStoreAsync(int storeId);
        public Task<CommandResult> GetAllStoresUserIsInByUserId(int userId);
        public Task LikeProfessional(LikeStoreProfessionalCommand command);
    }
}