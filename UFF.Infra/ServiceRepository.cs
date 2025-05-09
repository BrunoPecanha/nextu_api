using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class ServiceRepository : RepositoryBase<Domain.Entity.Service>, IServiceRepository
    {
        private readonly IUffContext _dbContext;

        public ServiceRepository(IUffContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entity.Service>> GetAllAsync(int storeId)
            => await _dbContext.Service
                             .Where(x => x.StoreId == storeId)
                             .OrderBy(x => x.Id)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<Domain.Entity.Service> GetByIdAsync(int id)
            => await _dbContext.Service
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
    }
}