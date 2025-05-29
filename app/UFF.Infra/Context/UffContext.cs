using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using UFF.Domain.Entity;
using UFF.Infra.Extensions;

namespace UFF.Infra.Context
{
    public class UffContext : DbContext, IUffContext
    {
        private static string connectionString;
        public UffContext(DbContextOptions<UffContext> options, IConfiguration configuration)
                : base(options)
        {
            if (connectionString is null)
            {
                connectionString = configuration.GetSection("ConnectionStrings:postgresConnection").Value;
            }
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerService> CustomerService { get; set; }
        public DbSet<HighLight> HighLight { get; set; }
        public DbSet<OpeningHours> OpeningHour { get; set; }
        public DbSet<Queue> Queue { get; set; }
        public DbSet<ServiceCategory> ServiceCategory { get; set; }
        public DbSet<Domain.Entity.Service> Service { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<EmployeeStore> EmployeeStore { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {                
                entity.SetTableName(entity.GetTableName().ToSnakeCase());
             
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }
                
                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UffContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}