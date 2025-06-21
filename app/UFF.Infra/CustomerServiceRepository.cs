using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class CustomerServiceRepository : RepositoryBase<Domain.Entity.CustomerService>, ICustomerServiceRepository
    {
        private readonly IUffContext _dbContext;

        public CustomerServiceRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerService[]> GetCustomersSelectedServices(int customerId)
        => await _dbContext.CustomerService
                       .Include(x => x.Queue)
                       .Include(x => x.Service)
                       .Where(x => x.CustomerId == customerId)
                       .ToArrayAsync();
    }
}