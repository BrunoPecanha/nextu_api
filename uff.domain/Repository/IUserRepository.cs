using UFF.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UFF.Domain.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<User> GetByIdAsync(int id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetUserByLogin(string userName);
    }
}
