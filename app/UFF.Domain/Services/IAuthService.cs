using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Entity;

namespace UFF.Domain.Services
{
    public interface IAuthService
    {
        Task<CommandResult> AuthSync(string email, string password);
        Task<CommandResult> RefreshTokenAsync(string refreshToken);
        Task<CommandResult> RevokeRefreshTokenAsync(string refreshToken);
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string providedPassword);
    }
}
