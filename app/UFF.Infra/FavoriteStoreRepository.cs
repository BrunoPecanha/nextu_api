using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{
    public class FavoriteStoreRepository : RepositoryBase<FavoriteStore>, IFavoriteStoreRepository
    {
        private readonly IUffContext _dbContext;

        public FavoriteStoreRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FavoriteStore[]> GetAllLikedStoresByUser(int userId)
            => await _dbContext.FavoriteStore
                             .Where(x => x.UserId == userId)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<FavoriteStore> GetStoreLikedByUser(int userId, int storeId)
          => await _dbContext.FavoriteStore
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.UserId == userId && x.StoreId == storeId);
    }
}
