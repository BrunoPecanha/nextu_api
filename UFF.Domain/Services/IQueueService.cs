using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Queue;

namespace UFF.Domain.Services
{
    public interface IQueueService
    {
        public Task<CommandResult> CreateAsync(QueueCreateCommand command);
        public Task<CommandResult> UpdateAsync(QueueEditCommand command);
        public Task<CommandResult> GetAllByStoreIdAsync(int idStore);
        public Task<CommandResult> GetByDateAsync(int idStore, DateTime date);
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> GetOpenedQueueByEmployeeId(int id);
        public Task<CommandResult> GetCustomerInQueueCardByCustomerId(int userId);
        public Task<CommandResult> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId);
        public Task<CommandResult> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId);
        public Task<CommandResult> DeleteAsync(int id);
    }
}