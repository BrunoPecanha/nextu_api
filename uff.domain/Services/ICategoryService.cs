using System.Threading.Tasks;
using UFF.Domain.Commands;

namespace UFF.Domain.Services
{
    public interface ICategoryService
    {
        public Task<CommandResult> CreateAsync(object command);
        public Task<CommandResult> UpdateAsync(object command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}