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

            builder.HasOne(qc => qc.Customer)
                .WithMany(c => c.QueueCustomers)
                .HasForeignKey(qc => qc.CustomerId)
                .HasConstraintName("fk_queue_customers_customers")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(qc => qc.Position)
                .HasColumnName("position")
                .IsRequired();

            builder.Property(qc => qc.TimeEnteredQueue)
                .HasColumnName("time_entered_queue")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(qc => qc.TimeCalledInQueue)
                .HasColumnName("time_called_in_queue")
                .HasColumnType("timestamp with time zone");

            builder.Property(qc => qc.ServiceStartTime)
                .HasColumnName("service_start_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(qc => qc.ServiceEndTime)
                .HasColumnName("service_end_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(qc => qc.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(30)");

            builder.Property(qc => qc.IsPriority)
                .HasColumnName("is_priority")
                .IsRequired()
                .HasDefaultValue(false);


            builder.HasIndex(qc => qc.QueueId)
                   .HasDatabaseName("ix_queue_customers_queue_id");

            builder.HasIndex(qc => qc.CustomerId)
                   .HasDatabaseName("ix_queue_customers_customer_id");

            builder.HasIndex(qc => qc.Status)
                   .HasDatabaseName("ix_queue_customers_status");

            builder.HasIndex(qc => qc.Position)
                   .HasDatabaseName("ix_queue_customers_position");

            builder.HasIndex(qc => qc.TimeEnteredQueue)
                   .HasDatabaseName("ix_queue_customers_time_entered");


            builder.HasIndex(qc => new { qc.QueueId, qc.Position })
                   .HasDatabaseName("ix_queue_customers_queue_position")
                   .IsDescending(false, true);
        }
    }
}