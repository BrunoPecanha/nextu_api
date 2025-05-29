using System.Threading.Tasks;

namespace UFF.Domain.Repository
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
