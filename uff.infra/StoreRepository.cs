using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        private readonly IUffContext _dbContext;

        public StoreRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO - REFATORAR ESSE MÉTODO, POIS VAI FICAR PESADO
        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            var stores = await _dbContext.Store
                .AsNoTracking()
                .Include(s => s.Owner)
                .AsNoTracking()
                .Include(s => s.Category)
                .OrderByDescending(s => s.RegisteringDate)
                .ToListAsync();

            foreach (var store in stores)
            {
                var smallestQueue = await GetSmallestQueueAsync(store.Id);
                store.SetSmallestQueue(smallestQueue != null ? new List<Queue> { smallestQueue } : new List<Queue>());
            }

            return stores;
        }

        public async Task<Store> GetByIdAsync(int id)
            => await _dbContext.Store
                .Include(x => x.HighLights)
                .Include(o => o.OpeningHours)
                .Include(x => x.Owner)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

        private async Task<Queue> GetSmallestQueueAsync(int storeId)
        {
            return await _dbContext.Queue
                .Where(q => q.StoreId == storeId)
                .Include(q => q.Customers.Where(c => c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent))
                .OrderBy(q => q.Customers.Count(c => c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Store> GetByIdWithProfessionalsAsync(int id)
            => await _dbContext.Store
                               .Include(x => x.Category)
                               .Include(y => y.EmployeeStore)
                               .ThenInclude(x => x.Employee)
                               .AsNoTracking()
                               .Include(x => x.Owner)
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Store[]> GetByCategoryId(int id)
        {
            var stores = await _dbContext.Store
                .Include(x => x.Category)
                           .Where(x => x.Category.Id == id)
                           .AsNoTracking()
                           .ToArrayAsync();

            foreach (var store in stores)
            {
                var smallestQueue = await GetSmallestQueueAsync(store.Id);
                store.SetSmallestQueue(smallestQueue != null ? new List<Queue> { smallestQueue } : new List<Queue>());
            }

            return stores;
        }

        public async Task<Store[]> GetByOwnerIdAsync(int id)
            => await _dbContext.Store
                           .Include(x => x.Category)
                           .Include(x => x.Owner)
                           .Where(x => x.Owner.Id == id)
                           .AsNoTracking()
                           .ToArrayAsync();

        public async Task<Store[]> GetByEmployeeId(int id)
          => await _dbContext.EmployeeStore
                         .Include(x => x.Store)
                         .ThenInclude(y => y.Category)
                         .Where(x => x.EmployeeId == id)
                         .AsNoTracking()
                         .Select(x => x.Store)
                         .ToArrayAsync();

        public async Task<Store> GetStoreWithEmployeesAndQueuesAsync(int storeId)
        {
            var today = DateTime.UtcNow.Date;

            return await _dbContext.Store
                .Where(s => s.Id == storeId)
                .Include(s => s.EmployeeStore)
                    .ThenInclude(es => es.Employee)
                        .ThenInclude(e => e.Queues
                            .Where(q => (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) && q.Date.Date == today))
                .Include(s => s.Queues
                    .Where(q => (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) && q.Date.Date == today))
                    .ThenInclude(q => q.Employee)
                .Include(s => s.Queues
                    .Where(q => (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) && q.Date.Date == today))
                    .ThenInclude(q => q.Customers
                        .Where(c => c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }


        public async Task<IList<User>> GetProfessionalsOfStoreAsync(int storeId)
        {
            return await _dbContext.Customer
                .Where(c => c.Queue.Employee.Stores.Any(s => s.Id == storeId))
                .Select(c => c.Queue.Employee)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Queue> CalculateAverageWaitingTime(int professionalId)
        {
            return await _dbContext.Customer
                .Include(c => c.CustomerServices)
                .Include(q => q.Queue)
                    .ThenInclude(k => k.Customers
                         .Where(o => (o.Status == CustomerStatusEnum.Waiting || o.Status == CustomerStatusEnum.Absent)))
                    .ThenInclude(x => x.CustomerServices)
                .Where(q => q.Queue.EmployeeId == professionalId && q.Queue.Status == QueueStatusEnum.Open && q.Status == CustomerStatusEnum.Waiting)
                .Select(q => q.Queue)
                .FirstOrDefaultAsync();
        }
    }
}