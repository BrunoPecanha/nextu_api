using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Service;

namespace UFF.Domain.Services
{
    public interface IServiceService
    {
        public Task<CommandResult> CreateAsync(ServiceCreateCommand command);
        public Task<CommandResult> UpdateAsync(ServiceEditCommand command);
        public Task<CommandResult> GetAllAsync(int storeId);
        public Task<CommandResult> GetAllCategoriesAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}