using System.Threading.Tasks;
using uff.Domain.Commands;

namespace uff.domain.Services
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