using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;

namespace UFF.Domain.Services
{
    public interface ICustomerService
    {
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> UpdateAsync(CustomerEditServicesPaymentCommand command);
    }
}