using Supply.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Domain
{
    public interface ICostumerRepository : IRepositoryBase<Costumer> {
        Task<Costumer> GetByIdAsync(int id);
        Task<IEnumerable<Costumer>> GetAllAsync();       
    }
}
