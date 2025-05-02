using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
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
                             .Include(x => x.Category)
                             .ToArrayAsync();

        public async Task<Store> GetByIdAsync(int id)
            => await _dbContext.Store
                               .Include(x => x.Category)
                               .AsNoTracking()
                               .Include(x => x.Owner)
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Store[]> GetByCategoryId(int id)
            => await _dbContext.Store
                           .Include(x => x.Category)
                           .Where(x => x.Category.Id == id)
                           .AsNoTracking()
                           .ToArrayAsync();

        public async Task<Store[]> GetByOwnerIdAsync(int id)
            => await _dbContext.Store
                           .Include(x => x.Category)
                           .Include(x => x.Owner)
                           .Where(x => x.Owner.Id == id)
                           .AsNoTracking()
                           .ToArrayAsync();

        public async Task<Store[]> GetByEmployeeId(int id)
          => await _dbContext.EmployeeStore
                         .Include(x => x.Store)
                         .ThenInclude(y => y.Category)
                         .Where(x => x.EmployeeId == id)
                         .AsNoTracking()
                         .Select(x => x.Store)
                         .ToArrayAsync();
    }
}
