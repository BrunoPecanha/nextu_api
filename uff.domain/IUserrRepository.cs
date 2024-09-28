using uff.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uff.Domain
{
    public interface IUserRepository : IRepositoryBase<User> {
        public Task<User> GetByIdAsync(int id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetUserByLogin(string userName);
    }
}
