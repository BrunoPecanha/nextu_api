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

        public CustomerRepository(UffContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetByIdReducedAsync(int id)
            => await _dbContext.Customer
                          .Include(x => x.Queue)
                          .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Customer> GetByIdAsync(int id)
           => await _dbContext.Customer
                         .Include(x => x.User)
                         .Include(x => x.CustomerServices)
                            .ThenInclude(x => x.Service)
                            .ThenInclude(x => x.Category)
                         .Include(x => x.Queue)
                         .Include(x => x.Payment)
                         .Include(x => x.CustomerServices)
                         .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> GetByLasPositionInQueueByStoreAndEmployeeIdAsync(int store, int employee)
        {
            var lastCustomer = await _dbContext.Customer
                .AsNoTracking()
                .Where(x => x.Queue.StoreId == store && x.Queue.EmployeeId == employee
                && x.Status == Domain.Enum.CustomerStatusEnum.Waiting || x.Status == Domain.Enum.CustomerStatusEnum.Absent)
                .OrderBy(x => x.Position)
                .LastOrDefaultAsync();

            return lastCustomer?.Position + 1 ?? 1;
        }
    }
}
