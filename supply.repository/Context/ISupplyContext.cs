using Microsoft.EntityFrameworkCore;
using Supply.Domain.Entity;

namespace Supply.Repository.Context
{
    public interface ISupplyContext {
        DbSet<Costumer> Costumer { get; }
        int SaveChanges();
    }
}
