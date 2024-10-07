using uff.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uff.Domain
{
    public interface IStoreRepository : IRepositoryBase<Store> {
        public Task<Store> GetByIdAsync(int id);
        public Task<IEnumerable<Store>> GetAllAsync();
    }
}
