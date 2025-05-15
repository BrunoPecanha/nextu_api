using Microsoft.EntityFrameworkCore;
using UFF.Domain.Entity;

namespace UFF.Infra.Context
{
    public interface IUffContext
    {
        DbSet<Category> Category { get; }
        DbSet<Customer> Customer { get; }
        DbSet<CustomerService> CustomerService { get; }
        DbSet<HighLight> HighLight { get; }
        DbSet<OpeningHours> OpeningHour { get; }
        DbSet<Queue> Queue { get; }
        DbSet<ServiceCategory> ServiceCategory { get; }
        DbSet<Domain.Entity.Service> Service { get; }
        DbSet<Store> Store { get; }
        DbSet<User> User { get; }

        DbSet<EmployeeStore> EmployeeStore { get; }

        int SaveChanges();
    }
}
