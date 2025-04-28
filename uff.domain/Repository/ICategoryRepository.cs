using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface ICategoryRepository : IRepositoryBase<Category> {
        public Task<Category> GetByIdAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}