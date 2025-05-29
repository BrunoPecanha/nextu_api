using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.Infra.EntityConfig
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Domain.Entity.Service>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Service> builder)
        {
            // Tabela em snake_case
            builder.ToTable("services")
                   .HasKey(s => s.Id)
                   .HasName("pk_services");

        
            builder.Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.Property(s => s.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(10,2)") 
                .IsRequired();

            builder.Property(s => s.Duration)
                .HasColumnName("duration")
                .HasColumnType("interval")  
                .IsRequired();

            builder.Property(s => s.ImgPath)
                .HasColumnName("img_path")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(s => s.RegisteringDate)
                .HasColumnName("registering_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(s => s.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(s => s.VariableTime)
                .HasColumnName("variable_time")
                .HasDefaultValue(false);

            builder.Property(s => s.VariablePrice)
                .HasColumnName("variable_price")
                .HasDefaultValue(false);

            builder.Property(s => s.Activated)
                .HasColumnName("activated")
                .HasDefaultValue(true);

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId)
                .HasConstraintName("fk_services_category")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Store)
                .WithMany(s => s.Services)
                .HasForeignKey(s => s.StoreId)
                .HasConstraintName("fk_services_store")
                .OnDelete(DeleteBehavior.Restrict);
                        
            builder.HasIndex(s => s.Name)
                .HasDatabaseName("ix_services_name");

            builder.HasIndex(s => s.CategoryId)
                .HasDatabaseName("ix_services_category_id");

            builder.HasIndex(s => s.StoreId)
                .HasDatabaseName("ix_services_store_id");

            builder.HasIndex(s => s.Activated)
                .HasDatabaseName("ix_services_activated");
                        
            builder.HasIndex(s => new { s.StoreId, s.Activated })
                .HasDatabaseName("ix_services_store_active");
        }
    }
}