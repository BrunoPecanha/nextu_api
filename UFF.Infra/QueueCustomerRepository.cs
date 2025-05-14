using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class QueueCustomerRepository : RepositoryBase<QueueCustomer>, IQueueCustomerRepository
    {
        private readonly IUffContext _dbContext;

        public QueueCustomerRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}