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

        public QueueRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO - Paginar isso
        public async Task<IEnumerable<Queue>> GetAllByStoreIdAsync(
                                                            int storeId,
                                                            int? responsableId,
                                                            QueueStatusEnum? queueStatus,
                                                            DateTime? startDate,
                                                            DateTime? endDate)
        {
            var query = _dbContext.Queue
                .Include(x => x.Customers)
                .Where(x => x.StoreId == storeId)
                .AsNoTracking();

            if (startDate.HasValue && endDate.HasValue)
                query = query.Where(x => x.Date >= startDate && x.Date <= endDate);

            if (responsableId.HasValue)
                query = query.Where(x => x.EmployeeId == responsableId);

            if (queueStatus.HasValue)
                query = query.Where(x => x.Status == queueStatus);

            return await query.OrderByDescending(x => x.Status).ToArrayAsync();
        }


        public async Task<Queue> GetByIdWithStoreAsync(int id)
            => await _dbContext.Queue
                               .Include(x => x.Store)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Queue> GetByIdAsync(int id)
           => await _dbContext.Queue
                              .Include(x => x.Store)
                              .AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Customer[]> GetCustomersByQueueId(int id)
           => await _dbContext.Customer
                              .AsNoTracking()
                              .Where(x => x.QueueId == id)
                              .ToArrayAsync();

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
            => await _dbContext.Customer
                .Include(x => x.Queue)
                    .ThenInclude(x => x.Store)
                .Include(x => x.Queue)
                    .ThenInclude(x => x.Employee)
                .Include(x => x.Payment)
                .Include(x => x.User)
                .Include(c => c.CustomerServices)
                    .ThenInclude(cs => cs.Service)
                .Where(x =>
                    x.Queue.EmployeeId == employeeId &&
                    x.Queue.Store.Id == storeId &&
                    (x.Queue.Status == QueueStatusEnum.Open || x.Queue.Status == QueueStatusEnum.Paused) &&
                    (
                        x.Status == CustomerStatusEnum.Waiting ||
                        x.Status == CustomerStatusEnum.InService ||
                        x.Status == CustomerStatusEnum.Absent
                    )
                )
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
                  .Where(x => (x.Status == CustomerStatusEnum.Waiting
                  || x.Status == CustomerStatusEnum.Absent)
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
                .FirstOrDefaultAsync(y => y.Id == customerId && y.QueueId == queueId && (y.Status == CustomerStatusEnum.Waiting || y.Status == CustomerStatusEnum.Absent));

        public async Task<(int TotalCustomers, TimeSpan EstimatedWaitTime)> GetQueueStatusAsync(int queueId, int currentCustomerPosition, int currentCustomerId)
        {
            var queue = await _dbContext.Queue
                .AsNoTracking()
                .Where(q => q.Id == queueId && q.Status == QueueStatusEnum.Open)
                .Select(q => new
                {
                    Customers = q.Customers
                        .Where(c =>
                            (c.Status == CustomerStatusEnum.Waiting ||
                             c.Status == CustomerStatusEnum.Absent ||
                             c.Status == CustomerStatusEnum.InService))
                        .Select(c => new
                        {
                            c.Id,
                            c.Status,
                            c.Position,
                            c.ServiceStartTime,
                            Services = c.CustomerServices.Select(cs => cs.Service.Duration.TotalMinutes)
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (queue == null)
                return (0, TimeSpan.Zero);

            double totalMinutesToWait = 0;

            foreach (var customer in queue.Customers)
            {
                if (customer.Id == currentCustomerId)
                    continue;

                if (customer.Position >= currentCustomerPosition)
                    continue;

                var customerTotalMinutes = customer.Services.Sum();

                if (customer.Status == CustomerStatusEnum.InService && customer.ServiceStartTime != null)
                {
                    var elapsed = DateTime.UtcNow - customer.ServiceStartTime.Value;
                    var remaining = customerTotalMinutes - elapsed.TotalMinutes;
                    totalMinutesToWait += Math.Max(remaining, 0);
                }
                else
                {
                    totalMinutesToWait += customerTotalMinutes;
                }
            }
            int totalCustomersAhead = queue.Customers.Count(c => c.Id != currentCustomerId && c.Position < currentCustomerPosition);

            return (totalCustomersAhead + 1, TimeSpan.FromMinutes(totalMinutesToWait));
        }

        public async Task<Customer[]> GetQueueReport(int id)
            => await _dbContext.Customer
              .Include(x => x.User)
              .Include(x => x.Payment)
              .Include(g => g.CustomerServices)
              .Include(x => x.Queue)
              .AsNoTracking()
              .Where(x => x.QueueId == id)
              .OrderBy(x => x.ServiceStartTime)
              .ToArrayAsync();

        public async Task<bool> ExistCustuomerInQueueWaiting(int id)
            => await _dbContext.Queue
                              .Include(c => c.Customers
                                    .Where(x => x.Status == CustomerStatusEnum.Waiting || x.Status == CustomerStatusEnum.Absent))
                              .AsNoTracking()
                              .AnyAsync(x => x.Id == id);
        
    }
}