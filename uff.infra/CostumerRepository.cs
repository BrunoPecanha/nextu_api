using Microsoft.EntityFrameworkCore;
using uff.Domain;
using uff.Domain.Entity;
using uff.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uff.Infra
{
    public class CostumerRepository : RepositoryBase<Costumer>, ICostumerRepository
    {
        private readonly IUffContext _dbContext;

        public CostumerRepository(IUffContext dbContext)
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
