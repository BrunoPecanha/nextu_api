using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class QueueCustomerConfiguration : IEntityTypeConfiguration<QueueCustomer>
    {
        public void Configure(EntityTypeBuilder<QueueCustomer> builder)
        {
            builder.ToTable("queue_customers");

            builder.HasKey(qc => new { qc.QueueId, qc.CustomerId })
                   .HasName("pk_queue_customers");

            builder.HasOne(qc => qc.Queue)
                .WithMany(q => q.QueueCustomers)
                .HasForeignKey(qc => qc.QueueId)
                .HasConstraintName("fk_queue_customers_queues")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.Id);

            builder.HasOne(qc => qc.Customer)
                .WithMany(c => c.QueueCustomers)
                .HasForeignKey(qc => qc.CustomerId)
                .HasConstraintName("fk_queue_customers_customers")
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}