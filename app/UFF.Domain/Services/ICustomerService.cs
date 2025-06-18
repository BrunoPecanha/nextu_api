using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;

namespace UFF.Domain.Services
{
    public interface ICustomerService
    {
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> UpdateAsync(CustomerEditServicesPaymentCommand command);
        public Task UpdatePriceAndTimeForVariableServiceAsync(CustomerVariablesCommand command);
        public Task<CommandResult> GetCustomerHistory(int userId, CustomerHistoryFilterCommand command);
        public Task<CommandResult> GetPendingOrdersCount(int storeId);
    }
}