using System.Threading.Tasks;
using UFF.Domain.Commands;

namespace UFF.Domain.Services
{
    public interface IServiceService
    {
        public Task<CommandResult> CreateAsync(object command);
        public Task<CommandResult> UpdateAsync(object command);
        public Task<CommandResult> GetAllAsync(int storeId);
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}