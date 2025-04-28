using System.Collections.Generic;
using System.Threading.Tasks;
using uff.Domain.Entity;

namespace uff.domain.Repository
{
    public interface ICategoryRepository : IRepositoryBase<Category> {
        public Task<Category> GetByIdAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}