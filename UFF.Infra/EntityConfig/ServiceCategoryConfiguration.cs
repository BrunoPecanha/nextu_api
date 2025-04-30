using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {

            builder.ToTable("service_categories")
                   .HasKey(sc => sc.Id)
                   .HasName("pk_service_categories");


            builder.Property(sc => sc.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(sc => sc.ImgPath)
                .HasColumnName("img_path")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);


            builder.Property(sc => sc.RegisteringDate)
                .HasColumnName("registering_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(sc => sc.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                .ValueGeneratedOnAddOrUpdate();


            builder.HasMany(sc => sc.Services)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .HasConstraintName("fk_services_category")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(sc => sc.Name)
                .HasDatabaseName("ix_service_categories_name")
                .IsUnique();

            builder.HasIndex(sc => sc.ImgPath)
                .HasDatabaseName("ix_service_categories_img_path")
                .HasFilter("img_path IS NOT NULL");
        }
    }
}