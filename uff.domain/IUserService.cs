using System.Threading.Tasks;
using uff.domain.Commands.User;
using uff.Domain.Commands;

namespace uff.Domain
{
    public interface IUserService { 
        public Task<CommandResult> CreateAsync(UserCreateCommand command);
        public Task<CommandResult> UpdateAsync(UserEditCommand command);
        public Task<CommandResult> GetAllAsync();
        public Task<CommandResult> GetByIdAsync(int id);
        public Task<CommandResult> DeleteAsync(int id);
    }
}