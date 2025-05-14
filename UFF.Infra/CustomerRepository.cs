using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IUffContext _dbContext;

        public CustomerRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetByIdAsync(int id)
            => await _dbContext.Customer
                          .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> GetByLasPositionInQueueByStoreAndEmployeeIdAsync(int store, int employee)
        {
            var lastCustomer = await _dbContext.Customer
                .AsNoTracking()
                .Where(x => x.Queue.StoreId == store && x.Queue.EmployeeId == employee)
                .OrderBy(x => x.Position)
                .LastOrDefaultAsync();

            return lastCustomer?.Position + 1 ?? 1;
        }
    }
}
