using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IEmployeeStoreRepository : IRepositoryBase<EmployeeStore> {
        public Task<EmployeeStore> GetByIdAsync(int id);
        public Task<IEnumerable<EmployeeStore>> GetAllAsync();
    }
}