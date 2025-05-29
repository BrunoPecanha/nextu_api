using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
{
    public class EmployeeStoreRepository : RepositoryBase<EmployeeStore>, IEmployeeStoreRepository
    {
        private readonly IUffContext _dbContext;

        public EmployeeStoreRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeStore>> GetAllAsync()
            => await _dbContext.EmployeeStore
                             .OrderBy(x => x.Id)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<EmployeeStore> GetByIdsAsync(int employeeId, int storeId)
            => await _dbContext.EmployeeStore
                               .Include(x => x.Store)
                               .Include(x => x.Employee)
                               .ThenInclude(x => x.Stores)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.StoreId == storeId);

        public async Task<EmployeeStore> GetByEmployeeAndStoreAndActivatedIds(int employeeId, int storeId)
            => await _dbContext.EmployeeStore
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.StoreId == storeId);

        public async Task<IEnumerable<EmployeeStore>> GetPendingAndAcceptedInvitesByUser(int id)
            => await _dbContext.EmployeeStore
                               .Include(x => x.Store)
                               .Include(x => x.Employee)
                                .AsNoTracking()
                                .Where(x => x.EmployeeId == id &&
                                            (!x.RequestAnswered || (x.RequestAnswered && x.IsActive)))
                                .ToListAsync();

        public async Task<IEnumerable<EmployeeStore>> GetPendingAndAcceptedInvitesByStore(int id)
           => await _dbContext.EmployeeStore
                               .Include(x => x.Store)
                               .Include(x => x.Employee)
                               .AsNoTracking()
                               .Where(x => x.StoreId == id &&
                                            (!x.RequestAnswered || (x.RequestAnswered && x.IsActive)))
                                .ToListAsync();
    }
}
