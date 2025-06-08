using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class FavoriteProfessionalConfiguration : IEntityTypeConfiguration<FavoriteProfessional>
    {
        public void Configure(EntityTypeBuilder<FavoriteProfessional> builder)
        {
            builder.ToTable("favorite_professional");

            builder.HasKey(f => new { f.UserId, f.ProfessionalId })
                .HasName("pk_favorite_professional");

            builder.HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .HasConstraintName("fk_favorite_professional_user")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Professional)
           .WithMany() 
           .HasForeignKey(f => f.ProfessionalId)
           .HasConstraintName("fk_favorite_professional_professional")
           .OnDelete(DeleteBehavior.Cascade);
        }
    }

}