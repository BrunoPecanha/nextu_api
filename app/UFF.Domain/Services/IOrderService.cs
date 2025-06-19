using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Order;

namespace UFF.Domain.Services
{
    public interface IOrderService
    {
        public Task<CommandResult> GetOrdersWatingApprovment(int storeId, int employeeId);
        Task<CommandResult> GetOrdersWatingApprovment(int storeId);
        Task<CommandResult> ProcessOrder(int id, OrderProcessCommand command);
    }
}