using System.Threading.Tasks;
using uff.Domain.Commands;
using uff.Domain.Entity;

namespace uff.domain.Repository
{
    public interface IAuthService
    {
        public Task<CommandResult> AuthSync(string userName, string password);
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string providedPassword);
    }
}
