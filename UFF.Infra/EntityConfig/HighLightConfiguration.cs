using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class HighLightConfiguration : IEntityTypeConfiguration<HighLight>
    {
        public void Configure(EntityTypeBuilder<HighLight> builder)
        {
            builder.ToTable("HighLights").HasKey(x => x.Id);

            builder.Property(hl => hl.Phrase)
                   .HasColumnName("Phrase")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(hl => hl.Icon)
                   .HasColumnName("Icon")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(hl => hl.RegisteringDate)
                   .HasColumnName("RegisteringDate")
                   .IsRequired();

            builder.Property(hl => hl.LastUpdate)
                   .HasColumnName("LastUpdate")
                   .IsRequired();

            builder.HasOne(hl => hl.Store)
                   .WithMany(s => s.HighLights)
                   .HasForeignKey(hl => hl.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}