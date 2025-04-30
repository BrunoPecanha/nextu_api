using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IQueueRepository : IRepositoryBase<Queue> {
        public Task<Queue> GetByIdAsync(int id);
        public Task<IEnumerable<Queue>> GetAllByStoreIdAsync(int storeId);
        Task<Queue[]> GetByDateAsync(DateTime date, int storeId);
    }
}