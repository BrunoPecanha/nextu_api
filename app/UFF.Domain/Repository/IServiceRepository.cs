using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IServiceRepository : IRepositoryBase<Service> {
        public Task<Service> GetByIdAsync(int id);
        public Task<IEnumerable<Service>> GetAllAsync(int storeId, bool onlyActivated = true);
        public Task<IEnumerable<ServiceCategory>> GetAllCategoriesAsync();
    }
}