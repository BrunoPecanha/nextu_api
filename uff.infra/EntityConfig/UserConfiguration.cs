using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
             .ToTable("User")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
             .Property(c => c.LastUpdate)
             .HasColumnName("LastUpdate")
             .IsRequired();

            builder
            .Property(c => c.Name)
            .HasColumnName("Name");

            builder
            .Property(c => c.LastName)
            .HasColumnName("LastName");

            builder
              .Property(c => c.StateId)
              .HasColumnName("StateId");

            builder
            .Property(c => c.Phone)
            .HasColumnName("Phone");

            builder
            .Property(c => c.Address)
            .HasColumnName("Address");

            builder
            .Property(c => c.Number)
            .HasColumnName("Number");

            builder
            .Property(c => c.City)
            .HasColumnName("City");

            builder
            .Property(c => c.Status)
            .HasColumnName("Status");

            builder
            .Property(c => c.Email)
            .HasColumnName("Email");

            builder
            .Property(c => c.Password)
            .HasColumnName("Password");

            builder
            .Property(c => c.Cpf)
            .HasColumnName("Cpf");
        }
    }
}
