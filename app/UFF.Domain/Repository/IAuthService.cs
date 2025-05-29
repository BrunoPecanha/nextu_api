using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IAuthService
    {
        public Task<CommandResult> AuthSync(string userName, string password);
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string providedPassword);
    }
}
