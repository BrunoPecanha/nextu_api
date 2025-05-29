using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;

namespace UFF.Domain.Repository
{
    public interface IEmployeeStoreRepository : IRepositoryBase<EmployeeStore> {
        public Task<EmployeeStore> GetByIdsAsync(int employeeId, int storeId);
        public Task<EmployeeStore> GetByEmployeeAndStoreAndActivatedIds(int employeeId, int storeId);
        public Task<IEnumerable<EmployeeStore>> GetAllAsync();
        public Task<IEnumerable<EmployeeStore>> GetPendingAndAcceptedInvitesByUser(int id);
        public Task<IEnumerable<EmployeeStore>> GetPendingAndAcceptedInvitesByStore(int id);
    }
}