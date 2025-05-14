using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
{

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly IUffContext _dbContext;

        public CategoryRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _dbContext.Category
                             .OrderBy(x => x.Id)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<Category> GetByIdAsync(int id)
            => await _dbContext.Category
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
    }   
}
