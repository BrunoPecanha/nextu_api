using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
        public Task<RefreshToken> GetRefreshToken(string token);
        public Task RevokeRefreshToken(RefreshToken token);
        public Task UpdateRefreshToken(RefreshToken token);
        public Task<RefreshToken> GetByUserIdAsync(int userId);
        public Task RevokeAllRefreshTokens(int userId);
    }
}
