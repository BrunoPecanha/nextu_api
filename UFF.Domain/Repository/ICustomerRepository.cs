using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface ICustomerRepository : IRepositoryBase<Customer> {
        public Task<Customer> GetByIdAsync(int id);
        public Task<int> GetByLasPositionInQueueByStoreAndEmployeeIdAsync(int store, int employee);
    }
}