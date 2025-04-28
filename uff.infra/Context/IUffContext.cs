using Microsoft.EntityFrameworkCore;
using uff.Domain.Entity;

namespace uff.Infra.Context
{
    public interface IUffContext {
        DbSet<User> User { get; }
        DbSet<Store> Store { get; }

        DbSet<Category> Category { get; }
        int SaveChanges();
    }
}
