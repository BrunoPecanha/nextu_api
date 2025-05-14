using System.Threading.Tasks;

namespace UFF.Domain.Repository
{
    public interface IUnitOfWork 
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
    }
}
