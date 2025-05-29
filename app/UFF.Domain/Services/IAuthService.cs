using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain
{
    public interface IAuthService  {
        public Task<string> AuthSync(string userName, string password);
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string providedPassword);
    }
}
