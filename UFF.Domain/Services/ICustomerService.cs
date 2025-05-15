using System.Threading.Tasks;
using UFF.Domain.Commands;

namespace UFF.Domain.Services
{
    public interface ICustomerService
    {
        public Task<CommandResult> GetByIdAsync(int id);   
    }
}