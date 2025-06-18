using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface ICustomerRepository : IRepositoryBase<Customer> {
        public Task<Customer> GetByIdReducedAsync(int id);
        public Task<int> GetLastPositionInQueueByStoreAndEmployeeIdAsync(int store, int employee);
        public Task<Customer> GetByIdAsync(int id);
        public Task<Customer> GetOnlyCustomerByIdAsync(int id);
        public Task<List<Customer>> GetCustomerHistoryAsync(int id, DateTime startDate, DateTime endDate);
        public Task<int> GetPendingOrdersCount(int storeId);
    }
}