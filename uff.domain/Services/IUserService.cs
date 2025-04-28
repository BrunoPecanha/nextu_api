using System.Threading.Tasks;
using uff.Domain.Commands;
using uff.Domain.Commands.User;

namespace uff.domain.Services
{
    public interface IUserService
    {
        public Task<CommandResult> CreateAsync(UserCreateCommand command);
        public Task<CommandResult> UpdateAsync(UserEditCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}