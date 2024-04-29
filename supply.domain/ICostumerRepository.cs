using uff.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uff.Domain
{
    public interface ICostumerRepository : IRepositoryBase<Costumer> {
        public Task<Costumer> GetByIdAsync(int id);
        public Task<IEnumerable<Costumer>> GetAllAsync();       
    }
}
