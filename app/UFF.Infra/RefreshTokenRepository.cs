using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly IUffContext _dbContext;

        public RefreshTokenRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await _dbContext.RefreshToken
                .FirstOrDefaultAsync(x => x.Token == token);
        }       

        public async Task RevokeRefreshToken(RefreshToken token)
        {
            token.IsRevoked = true;
            _dbContext.RefreshToken.Update(token);
            _dbContext.SaveChanges();
        }

        public async Task UpdateRefreshToken(RefreshToken token)
        {
            _dbContext.RefreshToken.Update(token);
            _dbContext.SaveChanges();
        }

        public async Task<RefreshToken> GetByUserIdAsync(int userId)
        {
            return await _dbContext.RefreshToken
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsRevoked);
        }

        public async Task RevokeAllRefreshTokens(int userId)
        {
            var tokens = await _dbContext.RefreshToken
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }

            _dbContext.RefreshToken.UpdateRange(tokens);
            _dbContext.SaveChanges();
        }

    }
}
