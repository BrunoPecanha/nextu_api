using Microsoft.EntityFrameworkCore;
using uff.Domain.Entity;

namespace uff.Repository.Context
{
    public interface IUffContext {
        DbSet<Costumer> Costumer { get; }
        int SaveChanges();
    }
}
