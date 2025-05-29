using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IServiceCategoryRepository : IRepositoryBase<ServiceCategory> {
        public Task<ServiceCategory> GetByIdAsync(int id);
        public Task<IEnumerable<ServiceCategory>> GetAllAsync();
    }
}