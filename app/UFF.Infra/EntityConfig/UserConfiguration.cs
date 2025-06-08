using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users")
                   .HasKey(x => x.Id)
                   .HasName("pk_users");


            builder.Property(u => u.RegisteringDate)
                   .HasColumnName("registering_date")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(u => u.LastUpdate)
                   .HasColumnName("last_update")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(u => u.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.LastName)
                   .HasColumnName("last_name")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.AcceptAwaysMinorQueue)
                   .HasColumnName("accept_aways_minor_queue")
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(u => u.Cpf)
                   .HasColumnName("cpf")
                   .HasColumnType("varchar(11)")
                   .HasMaxLength(11)
                   .IsRequired(false);


            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Subtitle)
                   .HasColumnName("subtitle")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100);

            builder.Property(u => u.ServicesProvided)
                 .HasColumnName("services_provided")
                 .HasColumnType("varchar(100)")
                 .HasMaxLength(100);

            builder.Property(u => u.Phone)
                   .HasColumnName("phone")
                   .HasColumnType("varchar(20)")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(u => u.Address)
                   .HasColumnName("address")
                   .HasColumnType("varchar(200)")
                   .HasMaxLength(200);

            builder.Property(u => u.Number)
                   .HasColumnName("number")
                   .HasColumnType("varchar(20)")
                   .HasMaxLength(20);

            builder.Property(u => u.City)
                   .HasColumnName("city")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100);

            builder.Property(u => u.StateId)
                   .HasColumnName("state_id")
                   .HasColumnType("char(2)")
                   .HasMaxLength(2);


            builder.Property(u => u.Password)
                   .HasColumnName("password")
                   .HasColumnType("varchar(255)")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(u => u.Status)
                   .HasColumnName("status")
                   .HasColumnType("varchar(20)")
                   .IsRequired();

            builder.HasMany(u => u.Queues)
                   .WithOne(q => q.Employee)
                   .HasForeignKey(q => q.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Stores)
                   .WithOne(s => s.Owner)
                   .HasForeignKey(s => s.OwnerId)
                   .HasConstraintName("fk_stores_owner")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CustomerInstances)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .HasConstraintName("fk_customers_user")
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(u => u.Email)
                   .HasDatabaseName("ix_users_email")
                   .IsUnique();

            builder.HasIndex(u => u.Cpf)
                   .HasDatabaseName("ix_users_cpf")
                   .IsUnique();

            builder.HasIndex(u => new { u.StateId, u.City })
                   .HasDatabaseName("ix_users_location");

            builder.HasIndex(u => u.Status)
                   .HasDatabaseName("ix_users_status");
        }
    }
}