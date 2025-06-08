using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IFavoriteStoreRepository : IRepositoryBase<FavoriteStore> {
        public Task<FavoriteStore[]> GetAllLikedStoresByUser(int userId);
        public Task<FavoriteStore> GetStoreLikedByUser(int userId, int storeId);
    }
}