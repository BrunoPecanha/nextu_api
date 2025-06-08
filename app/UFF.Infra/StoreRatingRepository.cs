using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
{

    public class StoreRatingRepository : RepositoryBase<StoreRating>, IStoreRatingRepository
    {
        private readonly IUffContext _dbContext;

        public StoreRatingRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StoreRating>> GetAllAsync()
            => await _dbContext.StoreRating
                             .Include(x => x.Store)
                             .Include(x => x.Professional)
                             .OrderBy(x => x.Id)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<StoreRating> GetByIdAsync(int id)
            => await _dbContext.StoreRating
                               .Include(x => x.Store)
                               .Include(x => x.Professional)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<StoreRating>> GetByStoreIdAsync(int id)
           => await _dbContext.StoreRating
                              .Include(x => x.Store)
                              .Include(x => x.Professional)
                              .AsNoTracking()
                              .Where(x => x.StoreId == id)
                              .ToArrayAsync();
    }
}
