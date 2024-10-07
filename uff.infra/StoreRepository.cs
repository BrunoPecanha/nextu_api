using Microsoft.EntityFrameworkCore;
using uff.Domain;
using uff.Domain.Entity;
using uff.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uff.Infra
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        private readonly IUffContext _dbContext;

        public StoreRepository(IUffContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
            => await _dbContext.Store
                             .OrderByDescending(x => x.RegisteringDate)
                             .AsNoTracking()
                             .Include(x => x.Owner)
                             .ToArrayAsync();

        public async Task<Store> GetByIdAsync(int id)
            => await _dbContext.Store
                               .AsNoTracking()
                               .Include(x => x.Owner)
                               .FirstOrDefaultAsync(x => x.Id == id);      
    }
}
