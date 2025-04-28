using System.Collections.Generic;
using System.Threading.Tasks;
using uff.Domain.Entity;

namespace uff.domain.Repository
{
    public interface IStoreRepository : IRepositoryBase<Store> {
        public Task<Store> GetByIdAsync(int id);
        public Task<IEnumerable<Store>> GetAllAsync();
    }
}
