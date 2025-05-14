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

            // Chave primária composta
            builder.HasKey(qc => new { qc.QueueId, qc.CustomerId })
                   .HasName("pk_queue_customers");

            // Relacionamento com Queue
            builder.HasOne(qc => qc.Queue)
                .WithMany(q => q.QueueCustomers)
                .HasForeignKey(qc => qc.QueueId)
                .HasConstraintName("fk_queue_customers_queues")
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Customer
            builder.HasOne(qc => qc.Customer)
                .WithMany()
                .HasForeignKey(qc => qc.CustomerId)
                .HasConstraintName("fk_queue_customers_customers")
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com User - CORRIGIDO
            builder.HasOne(qc => qc.User)
                .WithMany(u => u.QueueCustomers)
                .HasForeignKey(qc => qc.UserId) // <-- Garanta que está usando UserId
                .HasConstraintName("fk_queue_customers_users")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}