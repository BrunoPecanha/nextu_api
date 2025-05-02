using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IQueueRepository : IRepositoryBase<Queue> {
        public Task<Queue> GetByIdAsync(int id);
        public Task<IEnumerable<Queue>> GetAllByStoreIdAsync(int storeId);
        public Task<Queue[]> GetOpenedQueueByEmployeeId(int id);        
        public Task<Queue[]> GetByDateAsync(DateTime date, int storeId);
        public Task<Customer[]> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId);
    }
}