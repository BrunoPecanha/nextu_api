using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uff.Domain.Entity;

namespace uff.Repository.EntityConfig
{
    public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
    {
        public void Configure(EntityTypeBuilder<Costumer> builder)
        {
            builder
             .ToTable("Costumer")
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
            .Property(c => c.Phone)
            .HasColumnName("Phone");

            builder
            .Property(c => c.Street)
            .HasColumnName("Street");

            builder
            .Property(c => c.Number)
            .HasColumnName("Number");

            builder
            .Property(c => c.City)
            .HasColumnName("City");

            builder
            .Property(c => c.Active)
            .HasColumnName("Active");


        }
    }
}
