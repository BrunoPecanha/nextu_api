using System.Threading.Tasks;
using UFF.Domain.Commands;

namespace UFF.Domain.Services
{
    public interface IOrderService
    {
        public Task<CommandResult> GetOrdersWatingApprovment(int storeId, int employeeId);
        Task<CommandResult> GetOrdersWatingApprovment(int storeId);
    }
}