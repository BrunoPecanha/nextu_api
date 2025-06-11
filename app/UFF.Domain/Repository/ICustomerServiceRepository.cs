using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface ICustomerServiceRepository : IRepositoryBase<CustomerService>
    {
        public Task<CustomerService[]> GetCustomersSelectedServices(int customerId);
    }
}