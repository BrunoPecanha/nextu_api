using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IStoreRatingRepository : IRepositoryBase<StoreRating> {
        public Task<StoreRating> GetByIdAsync(int id);
        public Task<IEnumerable<StoreRating>> GetAllAsync();
        public Task<IEnumerable<StoreRating>> GetByStoreIdAsync(int id);
    }
}