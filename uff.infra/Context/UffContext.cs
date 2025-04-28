using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using uff.Domain.Entity;
using System;
using System.Linq;

namespace uff.Infra.Context
{
    public class UffContext : DbContext, IUffContext {
        private static string connectionString;        
        public UffContext(DbContextOptions<UffContext> options, IConfiguration configuration)
                : base(options) {            
            if (connectionString is null) {
                connectionString = configuration.GetSection("ConnectionStrings:postgresConnection").Value;
            }           
        }

        public DbSet<User> User { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UffContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {           
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(connectionString);
            }

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        public override int SaveChanges() {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null)) {
                if (entry.State == EntityState.Added) {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.Now;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                } else if (entry.State == EntityState.Modified) {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

    }
}
