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

        public QueueRepository(IUffContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO - Paginar isso
        public async Task<IEnumerable<Queue>> GetAllByStoreIdAsync(int storeId)
            => await _dbContext.Queue
                               .Where(x => x.StoreId == storeId)
                               .OrderByDescending(x => x.RegisteringDate)
                               .AsNoTracking()
                               .ToArrayAsync();

        public async Task<Queue> GetByIdAsync(int id)
            => await _dbContext.Queue
                               .Include(x => x.Id == id)
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


        /// <summary>
        /// Esse método só é chamado quando a fila está aberta
        /// </summary>
        /// <param name="storeId">Id do estabelecimento</param>
        /// <param name="employeeId">Id do profissional</param>
        /// <returns></returns>
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
                        && x.Queue.Status == QueueStatusEnum.Open)
                .AsNoTracking()
                .Select(x => x.Customer)
                .ToArrayAsync();
    }
}