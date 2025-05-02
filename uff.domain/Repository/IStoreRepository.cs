using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IStoreRepository : IRepositoryBase<Store> {
        public Task<Store> GetByIdAsync(int id);
        public Task<Store[]> GetByCategoryId(int id);        
        public Task<IEnumerable<Store>> GetAllAsync();
        public Task<Store[]> GetByOwnerIdAsync(int id);
        public Task<Store[]> GetByEmployeeId(int id);
    }
}
