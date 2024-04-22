using Microsoft.EntityFrameworkCore;
using Supply.Domain;
using Supply.Domain.Entity;
using Supply.Repository.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supply.Repository
{
    public class CostumerRepository : RepositoryBase<Costumer>, ICostumerRepository
    {
        private readonly ISupplyContext _dbContext;

        public CostumerRepository(ISupplyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Costumer>> GetAllAsync()
            => await _dbContext.Costumer
                             .OrderByDescending(x => x.RegisteringDate)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<Costumer> GetByIdAsync(int id)
            => await _dbContext.Costumer
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
    }
}
