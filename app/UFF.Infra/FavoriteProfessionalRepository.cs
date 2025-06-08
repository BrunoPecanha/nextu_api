using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class FavoriteProfessionalRepository : RepositoryBase<FavoriteProfessional>, IFavoriteProfessionalRepository
    {
        private readonly IUffContext _dbContext;

        public FavoriteProfessionalRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FavoriteProfessional[]> GetAllLikedProfessionalsByUser(int userId)
            => await _dbContext.FavoriteProfessional
                             .Where(x => x.UserId == userId)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<FavoriteProfessional> GetProfessionalLikedByUser(int userId, int professionalId)
         => await _dbContext.FavoriteProfessional
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProfessionalId == professionalId);
    }
}
