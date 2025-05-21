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

        public async Task<EmployeeStore> GetByIdAsync(int id)
            => await _dbContext.EmployeeStore
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
    }
}
