using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using UFF.Domain.Enum;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using System;

namespace UFF.Infra
{
    public class QueueRepository : RepositoryBase<Queue>, IQueueRepository
    {
        private readonly IUffContext _dbContext;

        public QueueRepository(UffContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO - Paginar isso
        public async Task<IEnumerable<Queue>> GetAllByStoreIdAsync(int storeId)
            => await _dbContext.Queue
                               .Where(x => x.StoreId == storeId)
                               .OrderByDescending(x => x.Date)
                               .AsNoTracking()
                               .ToArrayAsync();

        public async Task<Queue> GetByIdWithStoreAsync(int id)
            => await _dbContext.Queue
                               .Include(x => x.Store)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Queue> GetByIdAsync(int id)
           => await _dbContext.Queue
                              .AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Queue[]> GetByDateAsync(DateTime date, int storeId)
            => await _dbContext.Queue
                           .Where(x => x.StoreId == storeId
                            && x.Date.Date == date.Date)
                           .AsNoTracking()
                           .ToArrayAsync();

        public async Task<Queue[]> GetOpenedQueueByEmployeeId(int id)
             => await _dbContext.Queue
                            .Where(x => x.EmployeeId == id)
                            .AsNoTracking()
                            .OrderByDescending(x => x.Status == QueueStatusEnum.Open)
                            .ThenBy(x => x.Date)
                            .ToArrayAsync();

        public async Task<Customer[]> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId)
            => await _dbContext.QueueCustomer
                .Include(x => x.Queue)
                .Include(x => x.Customer)
                    .ThenInclude(c => c.User)
                .Include(x => x.Customer)
                    .ThenInclude(c => c.Payment)
                .Include(x => x.Customer)
                    .ThenInclude(c => c.CustomerServices)
                        .ThenInclude(cs => cs.Service)
                .Where(x => x.Queue.EmployeeId == employeeId
                        && x.Queue.Store.Id == storeId
                        && x.Queue.Status == QueueStatusEnum.Open
                        && x.Customer.Status == CustomerStatusEnum.Waiting ||
                x.Customer.Status == CustomerStatusEnum.InService || x.Customer.Status == CustomerStatusEnum.Absent)
                .AsNoTracking()
                .Select(x => x.Customer)
                .OrderBy(x => x.Position)
                .ToArrayAsync();

        public async Task<Customer[]> GetCustomerInQueueCardByUserId(int userId)
              => await _dbContext.Customer
                  .Include(x => x.Payment)
                  .Include(g => g.CustomerServices)
                  .Include(x => x.Queue)
                  .ThenInclude(x => x.Store)
                  .ThenInclude(o => o.Category)
                  .AsNoTracking()
                  .Where(x => x.Status == CustomerStatusEnum.Waiting
                  || x.Status == CustomerStatusEnum.Absent
                  && x.User.Id == userId)
                  .ToArrayAsync();       

        public async Task<Customer> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId)
            => await _dbContext.Customer
                .Include(x => x.User)
                .Include(x => x.Payment)
                .Include(x => x.Queue)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Queue)
                .ThenInclude(x => x.Store)
                .ThenInclude(o => o.Category)
                .Include(g => g.CustomerServices)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(y => y.Id == customerId && y.QueueId == queueId && y.Status == CustomerStatusEnum.Waiting || y.Status == CustomerStatusEnum.Absent);

        public async Task<(int TotalCustomers, TimeSpan EstimatedWaitTime)> GetQueueStatusAsync(int queueId, int currentCustomerPosition)
        {
            var query = _dbContext.Queue
                .AsNoTracking()
                .Where(q => q.Id == queueId && q.Status == QueueStatusEnum.Open);

            var totalCustomers = await query
                .SelectMany(q => q.QueueCustomers)
                .Where(x => x.Customer.Status == CustomerStatusEnum.Waiting || x.Customer.Status == CustomerStatusEnum.Absent)
                .CountAsync();

            var totalMinutes = await query
                .SelectMany(q => q.QueueCustomers)
                .Where(qc => qc.Customer.Status == CustomerStatusEnum.Waiting || qc.Customer.Status == CustomerStatusEnum.Absent)
                .Where(qc => qc.Customer.Position < currentCustomerPosition)
                .SelectMany(qc => qc.Customer.CustomerServices)
                .Select(cs => cs.Service)
                .SumAsync(s => s.Duration.TotalMinutes);

            return (totalCustomers, TimeSpan.FromMinutes(totalMinutes));
        }
    }
}