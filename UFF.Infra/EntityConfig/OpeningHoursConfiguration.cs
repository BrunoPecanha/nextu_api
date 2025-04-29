using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class OpeningHoursConfiguration : IEntityTypeConfiguration<OpeningHours>
    {
        public void Configure(EntityTypeBuilder<OpeningHours> builder)
        {
            builder.ToTable("OpeningHours").HasKey(x => x.Id);

            builder.Property(oh => oh.WeekDay)
                   .HasColumnName("DiaSemana")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(oh => oh.Start)
                   .HasColumnName("HoraAbertura");

            builder.Property(oh => oh.End)
                   .HasColumnName("HoraFechamento");

            builder.Property(oh => oh.RegisteringDate)
                   .HasColumnName("RegisteringDate")
                   .IsRequired();

            builder.Property(oh => oh.LastUpdate)
                   .HasColumnName("LastUpdate")
                   .IsRequired();

            builder.HasOne(oh => oh.Store)
                   .WithMany(s => s.OpeningHours)
                   .HasForeignKey(oh => oh.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}