using System.Threading.Tasks;
using uff.Domain.Entity;

namespace uff.Domain
{
    public interface IAuthService  {
        public Task<string> AuthSync(string userName, string password);
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string providedPassword);
    }
}
