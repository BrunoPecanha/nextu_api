using Microsoft.EntityFrameworkCore;
using uff.Domain.Entity;
using uff.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uff.domain.Repository;

namespace uff.Infra
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IUffContext _dbContext;

        public UserRepository(IUffContext dbContext)
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
    }
}
