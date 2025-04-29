using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store").HasKey(x => x.Id);
            
            builder.Property(c => c.RegisteringDate)
                   .HasColumnName("RegisteringDate")
                   .IsRequired();

            builder.Property(c => c.LastUpdate)
                   .HasColumnName("LastUpdate")
                   .IsRequired();

            builder.Property(c => c.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Address)
                   .HasColumnName("Address")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.Number)
                   .HasColumnName("Number")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.City)
                   .HasColumnName("City")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.State)
                   .HasColumnName("State")
                   .HasMaxLength(2)
                   .IsRequired();

            builder.Property(c => c.Cnpj)
                   .HasColumnName("Cnpj")
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Property(c => c.Votes)
                .HasColumnName("Votes");
            
            builder.Property(c => c.Rating)
                .HasColumnName("Rating");

            builder.Property(c => c.OpenAutomatic)
                   .HasColumnName("OpenAutomatic")
                   .IsRequired();

            builder.Property(c => c.StoreSubtitle)
                   .HasColumnName("StoreSubtitle")
                   .HasMaxLength(100);

            builder.Property(c => c.AcceptOtherQueues)
                   .HasColumnName("AcceptOtherQueues")
                   .IsRequired();

            builder.Property(c => c.AnswerOutOfOrder)
                   .HasColumnName("AnswerOutOfOrder")
                   .IsRequired();

            builder.Property(c => c.AnswerScheduledTime)
                   .HasColumnName("AnswerScheduledTime")
                   .IsRequired();

            builder.Property(c => c.TimeRemoval)
                   .HasColumnName("TimeRemoval");

            builder.Property(c => c.WhatsAppNotice)
                   .HasColumnName("WhatsAppNotice")
                   .IsRequired();

            builder.Property(c => c.LogoPath)
                   .HasColumnName("LogoPath")
                   .HasMaxLength(255);

            builder.Property(c => c.WallPaperPath)
                   .HasColumnName("WallPaperPath")
                   .HasMaxLength(255);

            builder.Property(c => c.Status)
                   .HasColumnName("Status")
                   .HasConversion<string>()
                   .IsRequired();
            
            builder.HasOne(s => s.Owner)
                   .WithMany(u => u.Stores)
                   .IsRequired()
                   .HasForeignKey(s => s.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Category)
                    .WithMany(u => u.Stores)
                    .IsRequired()
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.OpeningHours)
                   .WithOne(oh => oh.Store)
                   .HasForeignKey(oh => oh.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(s => s.HighLights)
                   .WithOne(hl => hl.Store)
                   .HasForeignKey(hl => hl.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}