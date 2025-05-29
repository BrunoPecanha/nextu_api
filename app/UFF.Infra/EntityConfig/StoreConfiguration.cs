using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {            
            builder.ToTable("stores")
                   .HasKey(x => x.Id)
                   .HasName("pk_stores");
            
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
            
            builder.Property(s => s.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.Cnpj)
                   .HasColumnName("cnpj")
                   .HasColumnType("varchar(14)")
                   .HasMaxLength(14)
                   .IsRequired();

            
            builder.Property(s => s.Address)
                   .HasColumnName("address")
                   .HasColumnType("varchar(200)")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(s => s.Number)
                   .HasColumnName("number")
                   .HasColumnType("varchar(20)")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(s => s.City)
                   .HasColumnName("city")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.State)
                   .HasColumnName("state")
                   .HasColumnType("char(2)")
                   .HasMaxLength(2)
                   .IsRequired();

            
            builder.Property(s => s.OpenAutomatic)
                   .HasColumnName("open_automatic")
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(s => s.AttendSimultaneously)
              .HasColumnName("attend_simultaneously")
              .IsRequired()
              .HasDefaultValue(false);

            builder.Property(s => s.Verified)
                .HasColumnName("verified")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.StoreSubtitle)
                   .HasColumnName("store_subtitle")
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100);

            builder.Property(s => s.AcceptOtherQueues)
                   .HasColumnName("accept_other_queues")
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(s => s.AnswerOutOfOrder)
                   .HasColumnName("answer_out_of_order")
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(s => s.AnswerScheduledTime)
                   .HasColumnName("answer_scheduled_time")
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(s => s.TimeRemoval)
                   .HasColumnName("time_removal")
                   .HasColumnType("integer");

            builder.Property(s => s.WhatsAppNotice)
                   .HasColumnName("whatsapp_notice")
                   .IsRequired()
                   .HasDefaultValue(false);

            
            builder.Property(s => s.LogoPath)
                   .HasColumnName("logo_path")
                   .HasColumnType("varchar(255)")
                   .HasMaxLength(255);

            builder.Property(s => s.WallPaperPath)
                   .HasColumnName("wallpaper_path")
                   .HasColumnType("varchar(255)")
                   .HasMaxLength(255);

            
            builder.Property(s => s.Rating)
                   .HasColumnName("rating")
                   .HasColumnType("numeric(3,2)")
                   .HasDefaultValue(0);

            builder.Property(s => s.Votes)
                   .HasColumnName("votes")
                   .HasDefaultValue(0);

            
            builder.Property(s => s.Status)
                   .HasColumnName("status")
                   .HasColumnType("varchar(20)")
                   .HasConversion<string>()
                   .IsRequired();

            
            builder.HasOne(s => s.Owner)
                   .WithMany(u => u.Stores)
                   .HasForeignKey(s => s.OwnerId)
                   .HasConstraintName("fk_stores_owner")
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Stores)
                   .HasForeignKey(s => s.CategoryId)
                   .HasConstraintName("fk_stores_category")
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(s => s.OpeningHours)
                   .WithOne(oh => oh.Store)
                   .HasForeignKey(oh => oh.StoreId)                  
                   .HasConstraintName("fk_opening_hours_store")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.HighLights)
                   .WithOne(hl => hl.Store)
                   .HasForeignKey(hl => hl.StoreId)
                   .HasConstraintName("fk_highlights_store")
                   .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(s => s.Name)
                   .HasDatabaseName("ix_stores_name");

            builder.HasIndex(s => s.Cnpj)
                   .HasDatabaseName("ix_stores_cnpj")
                   .IsUnique();

            builder.HasIndex(s => s.OwnerId)
                   .HasDatabaseName("ix_stores_owner_id");

            builder.HasIndex(s => s.CategoryId)
                   .HasDatabaseName("ix_stores_category_id");

            builder.HasIndex(s => s.Status)
                   .HasDatabaseName("ix_stores_status");
                        
            builder.HasIndex(s => new { s.State, s.City })
                   .HasDatabaseName("ix_stores_location");
        }
    }
}