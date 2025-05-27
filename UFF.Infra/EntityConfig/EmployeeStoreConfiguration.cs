using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class EmployeeStoreConfiguration : IEntityTypeConfiguration<EmployeeStore>
    {
        public void Configure(EntityTypeBuilder<EmployeeStore> builder)
        {
            builder.ToTable("employee_stores")
                   .HasKey(x => new { x.EmployeeId, x.StoreId })
                   .HasName("pk_employee_stores");
                        
            builder.Property(es => es.EmployeeId)
                   .HasColumnName("user_id")
                   .HasColumnType("integer")
                   .IsRequired();

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .HasColumnType("integer")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(es => es.StoreId)
                   .HasColumnName("store_id")
                   .HasColumnType("integer")
                   .IsRequired();

            builder.Property(es => es.RegisteringDate)
                   .HasColumnName("registering_date")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(es => es.LastUpdate)
                   .HasColumnName("last_update")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(es => es.IsActive)
                   .HasColumnName("is_active")
                   .HasColumnType("boolean")
                   .IsRequired();
                        
            builder.HasOne(es => es.Employee)
                   .WithMany(u => u.EmployeeStore)
                   .HasForeignKey(es => es.EmployeeId)
                   .HasConstraintName("fk_employee_stores_users")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(es => es.Store)
                   .WithMany(s => s.EmployeeStore)
                   .HasForeignKey(es => es.StoreId)
                   .HasConstraintName("fk_employee_stores_stores")
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(es => es.EmployeeId)
                   .HasDatabaseName("ix_employee_stores_user_id");

            builder.HasIndex(es => es.StoreId)
                   .HasDatabaseName("ix_employee_stores_store_id");

            builder.HasIndex(es => es.RegisteringDate)
                   .HasDatabaseName("ix_employee_stores_registering_date");

            builder.HasIndex(es => es.IsActive)
                   .HasDatabaseName("ix_employee_stores_is_active");
        }
    }
}