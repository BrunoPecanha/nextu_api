using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface ICustomerRepository : IRepositoryBase<Customer> {
        public Task<Customer> GetByIdReducedAsync(int id);
        public Task<int> GetLastPositionInQueueByStoreAndEmployeeIdAsync(int store, int employee);
        public Task<Customer> GetByIdAsync(int id);
    }
}