using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.Infra.EntityConfig
{
    public class CustomerServiceConfiguration : IEntityTypeConfiguration<Domain.Entity.CustomerService>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.CustomerService> builder)
        {
            builder.ToTable("customer_services");

            builder.HasKey(cs => new { cs.CustomerId, cs.ServiceId, cs.QueueId })
                   .HasName("pk_customer_services");

           // builder.Ignore(x => x.Id);

            builder.HasOne(cs => cs.Customer)
                .WithMany(c => c.Items)
                .HasForeignKey(cs => cs.CustomerId)
                .HasConstraintName("fk_customer_services_customers")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Service)
                .WithMany()
                .HasForeignKey(cs => cs.ServiceId)
                .HasConstraintName("fk_customer_services_services")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Queue)
                .WithMany()
                .HasForeignKey(cs => cs.QueueId)
                .HasConstraintName("fk_customer_services_queues")
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(cs => cs.FinalPrice)
                .HasColumnName("final_price")
                .HasColumnType("numeric(18,2)");

            builder.Property(cs => cs.Duration)
                .HasColumnName("duration")
                .HasColumnType("interval");

            // Índices 
            builder.HasIndex(cs => cs.CustomerId)
                .HasDatabaseName("ix_customer_services_customer_id");

            builder.HasIndex(cs => cs.ServiceId)
                .HasDatabaseName("ix_customer_services_service_id");

            builder.HasIndex(cs => cs.QueueId)
                .HasDatabaseName("ix_customer_services_queue_id");
        }
    }
}