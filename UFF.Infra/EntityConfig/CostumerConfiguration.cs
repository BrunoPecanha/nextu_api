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

            builder.Property(c => c.Notes)
                .HasColumnName("notes")
                .HasColumnType("text");

            builder.Property(c => c.Rating)
                .HasColumnName("rating")
                .HasColumnType("integer");

            builder.Property(c => c.Review)
                .HasColumnName("review")
                .HasColumnType("text");

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

            builder.HasOne(c => c.Payment)
                .WithMany()
                .HasForeignKey(c => c.PaymentId)
                .HasConstraintName("fk_customers_payments")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.PaymentId)
                .IsRequired();

            builder.HasMany(c => c.CustomerServices)
                .WithOne(cs => cs.Customer)
                .HasForeignKey(cs => cs.CustomerId)
                .HasConstraintName("fk_customer_services_customers")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(qc => qc.Position)
              .HasColumnName("position")
              .IsRequired();

            builder.Property(qc => qc.TimeEnteredQueue)
                .HasColumnName("time_entered_queue")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(qc => qc.MissingCustomerRemovalTime)
             .HasColumnName("missing_customer_removal_time")
             .HasColumnType("timestamp with time zone")
             .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(c => c.RemoveReason)
                .HasColumnName("remove_reason")
                .HasColumnType("text");

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


            builder.Property(qc => qc.RandomCustomerName)
                .HasColumnName("random_customer_name")
                .IsRequired(false)
                .HasColumnType("varchar(30)");

            builder.Property(qc => qc.IsPriority)
                .HasColumnName("is_priority")
                .IsRequired()
                .HasDefaultValue(false);


            builder.HasIndex(qc => qc.QueueId)
                   .HasDatabaseName("ix_queue_customers_queue_id");

            builder.HasIndex(qc => qc.Status)
                   .HasDatabaseName("ix_queue_customers_status");

            builder.HasIndex(qc => qc.Position)
                   .HasDatabaseName("ix_queue_customers_position");

            builder.HasIndex(qc => qc.TimeEnteredQueue)
                   .HasDatabaseName("ix_queue_customers_time_entered");

            builder.HasIndex(qc => new { qc.QueueId, qc.Position })
                   .HasDatabaseName("ix_queue_customers_queue_position")
                   .IsDescending(false, true);

            builder.HasIndex(c => c.QueueId)
                .HasDatabaseName("ix_customers_queue_id");

            builder.HasIndex(c => c.UserId)
                .HasDatabaseName("ix_customers_user_id");

            builder.HasIndex(c => c.PaymentId)
                .HasDatabaseName("ix_customers_payment_id");

            builder.HasIndex(c => c.Rating)
                .HasDatabaseName("ix_customers_rating")
                .HasFilter("rating IS NOT NULL");
        }
    }
}