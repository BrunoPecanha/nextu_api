using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class QueueConfiguration : IEntityTypeConfiguration<Queue>
    {
        public void Configure(EntityTypeBuilder<Queue> builder)
        {            
            builder.ToTable("queues")  
                   .HasKey(q => q.Id)
                   .HasName("pk_queues");
            
            builder.Property(q => q.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(q => q.Date)
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(q => q.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(20)") 
                .HasConversion<string>()
                .IsRequired();
      
            builder.HasOne(q => q.Store)
                .WithMany(s => s.Queues)
                .HasForeignKey(q => q.StoreId)
                .HasConstraintName("fk_queues_stores")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(q => q.Employee)
            .WithMany(u => u.Queues) 
            .HasForeignKey(q => q.EmployeeId)
            .HasConstraintName("fk_queues_users")
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.QueueCustomers)
                .WithOne(qc => qc.Queue)
                .HasForeignKey(qc => qc.QueueId)
                .HasConstraintName("fk_queue_customers_queues")
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(q => q.StoreId)
                .HasDatabaseName("ix_queues_store_id");

            builder.HasIndex(q => q.Date)
                .HasDatabaseName("ix_queues_date");

            builder.HasIndex(q => q.Status)
                .HasDatabaseName("ix_queues_status");
          
            builder.HasIndex(q => new { q.StoreId, q.Status })
                .HasDatabaseName("ix_queues_store_status");
        }
    }
}