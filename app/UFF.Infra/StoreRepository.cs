using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Infra.Context;
using UFF.Service.Helpers;

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

        //TODO - REFATORAR ESSE MÉTODO, POIS VAI FICAR PESADO // PAGINAR
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

        // PAGINAR
        public async Task<Store[]> GetFilteredStoresAsync(int? categoryId, string quickFilter, int? userId, int page = 1, int pageSize = 10)
        {
            var today = DateTime.UtcNow.Date;

            var query = _dbContext.Store
                .Include(s => s.Category)
                .Include(s => s.Favorites)
                .Include(s => s.Queues.Where(q => q.RegisteringDate.Date == today))
                    .ThenInclude(q => q.Customers)
                .Include(s => s.Ratings)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(s => s.CategoryId == categoryId.Value);
            }

            if (quickFilter == "recent")
            {
                var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
                query = query.Where(s => s.RegisteringDate >= sevenDaysAgo);
            }
            else if (quickFilter == "favorites" && userId.HasValue)
            {
                query = query.Where(s => s.Favorites.Any(f => f.UserId == userId));
            }

            var stores = await query.ToListAsync();

            if (quickFilter == "minorQueue")
            {
                stores = stores
                    .OrderByDescending(s => s.Queues.Sum(q => q.Customers.Count))
                    .ToList();
            }

            foreach (var store in stores)
            {
                var smallestQueue = await GetSmallestQueueAsync(store.Id);
                store.SetSmallestQueue(smallestQueue != null ? new List<Queue> { smallestQueue } : new List<Queue>());
            }

            return stores
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArray();
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
                .Where(q => q.StoreId == storeId && q.Status == QueueStatusEnum.Open)
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

        public async Task<Store[]> GetByEmployeeId(int id, ProfileEnum profile)
        {
            var query = _dbContext.EmployeeStore
                        .Include(x => x.Store)
                        .ThenInclude(y => y.Category)
                        .Where(x => x.IsActive)
                        .AsNoTracking();

            if (profile == ProfileEnum.Owner)
                query = query.Where(x => x.Store.OwnerId == id);
            else
                query = query.Where(x => x.EmployeeId == id);


            return await query
                        .GroupBy(x => x.Store.Id)
                        .Select(g => g.First().Store)
                        .ToArrayAsync();
        }

        public async Task<Store> GetStoreWithEmployeesAndQueuesAsync(int storeId)
        {
            var (startUtc, endUtc) = DateTimeHelper.GetUtcRangeForTodayInBrazil();

            return await _dbContext.Store
                .Where(s => s.Id == storeId)
                .Include(s => s.EmployeeStore)
                    .ThenInclude(es => es.Employee)
                        .ThenInclude(e => e.Queues
                            .Where(q =>
                                (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) &&
                                q.Date >= startUtc && q.Date < endUtc))
                .Include(s => s.Queues
                    .Where(q =>
                        (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) &&
                        q.Date >= startUtc && q.Date < endUtc))
                    .ThenInclude(q => q.Employee)
                .Include(s => s.Queues
                    .Where(q =>
                        (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused) &&
                        q.Date >= startUtc && q.Date < endUtc))
                    .ThenInclude(q => q.Customers
                        .Where(c => c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }


        public async Task<Store[]> GetAllStoresUserIsInByUserId(int userId)
        {
            var today = DateTime.UtcNow.Date;

            return await _dbContext.Queue
                .Where(q => (q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused)
                            && q.Date.Date == today
                            && q.Customers.Any(c => c.User.Id == userId
                                                    && (c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent)))
                .Select(q => q.Store)
                .Distinct()
                .AsNoTracking()
                .ToArrayAsync();
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
                .Include(c => c.Items)
                .Include(q => q.Queue)
                    .ThenInclude(k => k.Customers
                         .Where(o => (o.Status == CustomerStatusEnum.Waiting || o.Status == CustomerStatusEnum.Absent)))
                    .ThenInclude(x => x.Items)
                .Where(q => q.Queue.EmployeeId == professionalId && q.Queue.Status == QueueStatusEnum.Open && q.Status == CustomerStatusEnum.Waiting)
                .Select(q => q.Queue)
                .FirstOrDefaultAsync();
        }
    }
}