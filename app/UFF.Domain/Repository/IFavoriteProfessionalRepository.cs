using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IFavoriteProfessionalRepository : IRepositoryBase<FavoriteProfessional> {
        public Task<FavoriteProfessional[]> GetAllLikedProfessionalsByUser(int userId);
        public Task<FavoriteProfessional> GetProfessionalLikedByUser(int userId, int professionalId);
    }
}