using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IQueueRepository : IRepositoryBase<Queue>
    {
        public Task<Queue> GetByIdWithStoreAsync(int id);
        public Task<IEnumerable<Queue>> GetAllByStoreIdAsync(int storeId, DateTime startDate = default, DateTime endDate = default);
        public Task<Queue[]> GetOpenedQueueByEmployeeId(int id);
        public Task<Queue> GetByIdAsync(int id);
        public Task<Queue[]> GetByDateAsync(DateTime date, int storeId);
        public Task<Customer[]> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId);
        public Task<Customer[]> GetCustomerInQueueCardByUserId(int customerId);
        public Task<Customer> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId);
        Task<(int TotalCustomers, TimeSpan EstimatedWaitTime)> GetQueueStatusAsync(int queueId, int currentCustomerPosition, int currentCustomerId);
    }
}