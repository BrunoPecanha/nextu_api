using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class FavoriteStoreConfiguration : IEntityTypeConfiguration<FavoriteStore>
    {
        public void Configure(EntityTypeBuilder<FavoriteStore> builder)
        {
            builder.ToTable("favorite_store");

            builder.HasKey(f => new { f.UserId, f.StoreId })
                .HasName("pk_favorite_store");

            builder.HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .HasConstraintName("fk_favorite_store_user")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Store)
                .WithMany(s => s.Favorites)
                .HasForeignKey(f => f.StoreId)
                .HasConstraintName("fk_favorite_store_store")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}