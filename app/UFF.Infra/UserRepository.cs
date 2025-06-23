using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;
using UFF.Infra.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Domain.Enum;
using System;

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

        public async Task<User> GetByIdAsync(int id, params string[] includeProperties)
        {
            var query = _dbContext.User.AsNoTracking();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByLogin(string userName)
           => await _dbContext.User
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Email == userName);

        public async Task<User> GetUserByCpf(string cpf)
           => await _dbContext.User
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Cpf == cpf);

        public async Task<User> GetLooseCustomer()
           => await _dbContext.User
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.LooseCustomer);

        public async Task<int> GetUserInfoByIdAsync(int userId, ProfileEnum profile)
        {
            int result = 0;

            switch (profile)
            {
                case ProfileEnum.Customer:
                    result = await _dbContext.Customer
                        .AsNoTracking()
                        .CountAsync(qc => qc.UserId == userId && qc.Queue.Status == QueueStatusEnum.Open && (qc.Status == CustomerStatusEnum.Waiting || qc.Status == CustomerStatusEnum.Absent || qc.Status == CustomerStatusEnum.InService));
                    break;

                case ProfileEnum.Employee:
                    result = await _dbContext.Customer
                        .AsNoTracking()
                        .Where(q => q.Queue.EmployeeId == userId && q.Queue.Status == QueueStatusEnum.Open)
                        .CountAsync(x => x.Status == CustomerStatusEnum.Waiting || x.Status == CustomerStatusEnum.Absent || x.Status == CustomerStatusEnum.InService);
                    break;

                case ProfileEnum.Owner:
                    result = await _dbContext.Queue
                        .AsNoTracking()
                        .Where(q => q.Store.OwnerId == userId && q.Status == QueueStatusEnum.Open)
                        .CountAsync();
                    break;
            }

            return result;
        }      

    }
}
