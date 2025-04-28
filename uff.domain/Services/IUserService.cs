using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.User;

namespace UFF.Domain.Services
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