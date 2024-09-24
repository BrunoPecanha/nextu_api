using Microsoft.EntityFrameworkCore;
using uff.Domain.Entity;

namespace uff.Infra.Context
{
    public interface IUffContext {
        DbSet<Costumer> Costumer { get; }
        int SaveChanges();
    }
}
