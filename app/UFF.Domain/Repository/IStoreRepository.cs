using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Enum;

namespace UFF.Domain.Repository
{
    public interface IStoreRepository : IRepositoryBase<Store>
    {
        public Task<Store> GetByIdAsync(int id);
        public Task<Store[]> GetByCategoryId(int id);
        public Task<IEnumerable<Store>> GetAllAsync();
        public Task<Store[]> GetByOwnerIdAsync(int id);
        public Task<Store[]> GetByEmployeeId(int id, ProfileEnum profile);
        public Task<Store> GetStoreWithEmployeesAndQueuesAsync(int id);
        public Task<Queue> CalculateAverageWaitingTime(int professionalId);
        public Task<Store> GetByIdWithProfessionalsAsync(int id);
        public Task<Store[]> GetAllStoresUserIsInByUserId(int userId);
        public Task<IList<User>> GetProfessionalsOfStoreAsync(int storeId);
    }
}
