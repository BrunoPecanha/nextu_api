using System.Threading.Tasks;

namespace uff.Domain
{
    public interface IAuthService  {
        public Task<string> AuthSync(string userName, string password);         
    }
}
