using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;

namespace UFF.Domain.Services
{
    public interface IQueueService
    {
        public Task<CommandResult> AddCustomerToQueueAsync(QueueAddCustomerCommand command);
        public Task<CommandResult> StartCustomerService(int customerId);
        public Task<CommandResult> SetTimeCustomerWasCalledInTheQueue(int customerId);
        public Task<CommandResult> SetTimeCustomerServiceWasCompleted(int customerId);
        public Task<CommandResult> RemoveMissingCustomer(CustomerRemoveFromQueueCommand command);
        public Task<CommandResult> GetAllByDateAndStoreIdAsync(int idStore, QueueFilterRequestCommand command);
        public Task<CommandResult> GetAllByStoreIdAsync(int idStore);
        public Task<CommandResult> GetByDateAsync(int idStore, DateTime date);
        public Task<CommandResult> GetOpenedQueueByEmployeeId(int id);
        public Task<CommandResult> GetCustomerInQueueCardByCustomerId(int userId);
        public Task<CommandResult> GetQueueReport(int id);        
        public Task<CommandResult> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId);
        public Task<CommandResult> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId);
        public Task<CommandResult> ExitQueueAsync(int customerId, int queueId);
        public Task<CommandResult> CreateQueueAsync(QueueCreateCommand command);
        public Task<CommandResult> CloseQueueAsync(QueueCloseCommand command);
        public Task<bool> ExistCustuomerInQueueWaiting(int queueId);
        public Task<CommandResult> PauseQueueAsync(QueuePauseCommand command);
        public Task<CommandResult> Delete(int queueId);
        public Task<CommandResult> UpdateCustomerName(int customerId, string name);
    }
}