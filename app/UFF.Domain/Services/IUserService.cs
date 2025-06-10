using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.User;
using UFF.Domain.Enum;

namespace UFF.Domain.Services
{
    public interface IUserService
    {
        public Task<CommandResult> CreateAsync(UserCreateCommand command);
        public Task<CommandResult> UpdateAsync(UserEditCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> GetUserInfoByIdAsync(int id, ProfileEnum profile);
        public Task<CommandResult> DeleteAsync(int id);
    }
}