using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
             .ToTable("Category")
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
             .Property(c => c.ImgPath)
             .HasColumnName("ImgPath");
        }
    }
}