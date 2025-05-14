using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
{

    public class ServiceCategoryRepository : RepositoryBase<ServiceCategory>, IServiceCategoryRepository
    {
        private readonly IUffContext _dbContext;

        public ServiceCategoryRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceCategory>> GetAllAsync()
            => await _dbContext.ServiceCategory
                             .OrderBy(x => x.Id)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<ServiceCategory> GetByIdAsync(int id)
            => await _dbContext.ServiceCategory
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
    }   
}
