using UFF.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Enum;

namespace UFF.Domain.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<User> GetByIdAsync(int id, params string[] includeProperties);
        public Task<int> GetUserInfoByIdAsync(int id, ProfileEnum profile);        
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetUserByLogin(string userName);
        public Task<User> GetUserByCpf(string cpf);
        public Task<User> GetLooseCustomer();
    }
}
