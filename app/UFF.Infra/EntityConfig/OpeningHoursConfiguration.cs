using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class OpeningHoursConfiguration : IEntityTypeConfiguration<OpeningHours>
    {
        public void Configure(EntityTypeBuilder<OpeningHours> builder)
        {
            
            builder.ToTable("opening_hours")
                   .HasKey(x => x.Id)
                   .HasName("pk_opening_hours");
            
            builder.Property(oh => oh.WeekDay)
                   .HasColumnName("week_day")
                   .HasColumnType("varchar(20)")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(oh => oh.Start)
                   .HasColumnName("start_time")
                   .HasColumnType("time without time zone");

            builder.Property(oh => oh.End)
                   .HasColumnName("end_time")
                   .HasColumnType("time without time zone");

            builder.Property(oh => oh.RegisteringDate)
                   .HasColumnName("registering_date")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(oh => oh.LastUpdate)
                   .HasColumnName("last_update")
                   .HasColumnType("timestamp with time zone")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                   .ValueGeneratedOnAddOrUpdate();
                        
            builder.HasOne(oh => oh.Store)
                   .WithMany(s => s.OpeningHours)
                   .HasForeignKey(oh => oh.StoreId)
                   .HasConstraintName("fk_opening_hours_stores")
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(oh => oh.StoreId)
                   .HasDatabaseName("ix_opening_hours_store_id");

            builder.HasIndex(oh => oh.WeekDay)
                   .HasDatabaseName("ix_opening_hours_week_day");
    
            builder.HasIndex(oh => new { oh.WeekDay, oh.Start, oh.End })
                   .HasDatabaseName("ix_opening_hours_week_schedule");
        }
    }
}