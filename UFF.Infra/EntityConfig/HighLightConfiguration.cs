using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class HighLightConfiguration : IEntityTypeConfiguration<HighLight>
    {
        public void Configure(EntityTypeBuilder<HighLight> builder)
        {            
            builder.ToTable("highlights")
                   .HasKey(x => x.Id)
                   .HasName("pk_highlights");

            builder.Property(hl => hl.Phrase)
                   .HasColumnName("phrase")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(hl => hl.Icon)
                   .HasColumnName("icon")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(hl => hl.RegisteringDate)
                   .HasColumnName("registering_date")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(hl => hl.LastUpdate)
                   .HasColumnName("last_update")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                   .ValueGeneratedOnAddOrUpdate();

            
            builder.HasOne(hl => hl.Store)
                   .WithMany(s => s.HighLights)
                   .HasForeignKey(hl => hl.StoreId)
                   .HasConstraintName("fk_highlights_stores")
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(hl => hl.StoreId)
                   .HasDatabaseName("ix_highlights_store_id");

            builder.HasIndex(hl => hl.RegisteringDate)
                   .HasDatabaseName("ix_highlights_registering_date");
        }
    }
}