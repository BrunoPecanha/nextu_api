using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {            
            builder.ToTable("customers")
                   .HasKey(c => c.Id)
                   .HasName("pk_customers");

            // Propriedades
            builder.Property(c => c.Notes)
                .HasColumnName("notes")
                .HasColumnType("text");  

            builder.Property(c => c.Rating)
                .HasColumnName("rating")
                .HasColumnType("integer");

            builder.Property(c => c.Review)
                .HasColumnName("review")
                .HasColumnType("text");

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(30)") 
                .HasConversion<string>()
                .IsRequired();
                        
            builder.Property(c => c.RegisteringDate)
                .HasColumnName("registering_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(c => c.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                .ValueGeneratedOnAddOrUpdate();

            // Relacionamentos
            builder.HasOne(c => c.Queue)
                .WithMany()
                .HasForeignKey(c => c.QueueId)
                .HasConstraintName("fk_customers_queues")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User)
                .WithMany(u => u.CustomerInstances)
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("fk_customers_users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.QueueCustomers)
                .WithOne(qc => qc.Customer)
                .HasForeignKey(qc => qc.CustomerId)
                .HasConstraintName("fk_queue_customers_customers")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CustomerServices)
                .WithOne(cs => cs.Customer)
                .HasForeignKey(cs => cs.CustomerId)
                .HasConstraintName("fk_customer_services_customers")
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(c => c.QueueId)
                .HasDatabaseName("ix_customers_queue_id");

            builder.HasIndex(c => c.UserId)
                .HasDatabaseName("ix_customers_user_id");

            builder.HasIndex(c => c.Status)
                .HasDatabaseName("ix_customers_status");

            builder.HasIndex(c => c.Rating)
                .HasDatabaseName("ix_customers_rating")
                .HasFilter("rating IS NOT NULL");
        }
    }
}