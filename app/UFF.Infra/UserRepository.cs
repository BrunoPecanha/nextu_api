using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Infra
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IUffContext _dbContext;

        public UserRepository(UffContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _dbContext.User
                             .OrderByDescending(x => x.RegisteringDate)
                             .AsNoTracking()
                             .ToArrayAsync();

        public async Task<User> GetByIdAsync(int id)
            => await _dbContext.User
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetUserByLogin(string userName)
           => await _dbContext.User
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Email == userName);

        public async Task<User> GetUserByCpf(string cpf)
           => await _dbContext.User
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Cpf == cpf);
    }
}
