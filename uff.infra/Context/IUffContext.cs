using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;

namespace UFF.Infra.Context
{
    public interface IUffContext {
        DbSet<User> User { get; }
        DbSet<Store> Store { get; }

        DbSet<Category> Category { get; }
        int SaveChanges();
    }
}
