using System.Threading.Tasks;

namespace uff.domain.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T obj);
        void Update(T obj);
        void Remove(T obj);
        Task DisposeAsync();
        Task SaveChangesAsync();
    }
}
