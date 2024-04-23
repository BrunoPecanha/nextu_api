using Supply.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Domain
{
    public interface ICostumerRepository : IRepositoryBase<Costumer> {
        public Task<Costumer> GetByIdAsync(int id);
        public Task<IEnumerable<Costumer>> GetAllAsync();       
    }
}
