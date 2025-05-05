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

        public StoreRepository(IUffContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
            => await _dbContext.Store
                             .OrderByDescending(x => x.RegisteringDate)
                             .AsNoTracking()
                             .Include(x => x.Owner)
                             .Include(x => x.Category)
                             .ToArrayAsync();

        public async Task<Store> GetByIdAsync(int id)
            => await _dbContext.Store
                               .Include(x => x.Category)
                               .AsNoTracking()
                               .Include(x => x.Owner)
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Store> GetByIdWithProfessionalsAsync(int id)
            => await _dbContext.Store
                               .Include(x => x.Category)
                               .Include(y => y.EmployeeStore)
                               .ThenInclude(x => x.Employee)
                               .AsNoTracking()
                               .Include(x => x.Owner)
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Store[]> GetByCategoryId(int id)
            => await _dbContext.Store
                           .Include(x => x.Category)
                           .Where(x => x.Category.Id == id)
                           .AsNoTracking()
                           .ToArrayAsync();

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
                .Include(y => y.Queues
                    .Where(o => o.RegisteringDate.Date == today))
                .ThenInclude(k => k.Employee)                
                .Include(y => y.Queues
                    .Where(o => o.RegisteringDate.Date == today))
                .ThenInclude(o => o.QueueCustomers)
                .ThenInclude(x => x.Customer)                
                .Include(s => s.EmployeeStore)
                .ThenInclude(es => es.Employee)
                .Include(s => s.EmployeeStore)
                .ThenInclude(es => es.Employee)
                .ThenInclude(x => x.Queues
                    .Where(q => q.RegisteringDate.Date == today))
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == storeId);
        }

        public async Task<Queue> CalculateAverageWaitingTime(int professionalId)
        {
            return await _dbContext.Queue
                .Include(q => q.QueueCustomers
                    .Where(qc => qc.Customer.Status == CustomerStatusEnum.Waiting))
                .ThenInclude(qc => qc.Customer)
                .ThenInclude(c => c.CustomerServices)
                .FirstOrDefaultAsync(q => q.EmployeeId == professionalId && q.Status == QueueStatusEnum.Open);
        }        
    }
}