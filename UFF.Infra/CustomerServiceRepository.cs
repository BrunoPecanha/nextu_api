using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class CustomerServiceRepository : RepositoryBase<CustomerService>, ICustomerServiceRepository
    {
        private readonly IUffContext _dbContext;

        public CustomerServiceRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}