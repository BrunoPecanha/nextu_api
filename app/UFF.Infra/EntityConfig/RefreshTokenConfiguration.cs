using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infrastructure.Mapping
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens")
                   .HasKey(x => x.Id)
                   .HasName("pk_refresh_tokens");

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();

            builder.Property(x => x.Token)
                   .HasColumnName("token")
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(x => x.ExpiryDate)
                   .HasColumnName("expiry_date")
                   .IsRequired();

            builder.Property(x => x.IsRevoked)
                   .HasColumnName("is_revoked")
                   .IsRequired()
                   .HasDefaultValue(false);           

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .HasConstraintName("fk_refresh_tokens_users")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
