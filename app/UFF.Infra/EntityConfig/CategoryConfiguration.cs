using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("categories");

            builder.HasKey(x => x.Id)
                   .HasName("pk_categories");

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

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ImgPath)
                .HasColumnName("img_path")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(c => c.Icon)
             .HasColumnName("icon")
             .HasColumnType("varchar(15)")
             .HasMaxLength(255);

            builder.HasIndex(c => c.Name)
                .HasDatabaseName("ix_categories_name")
                .IsUnique();
        }
    }
}