using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.Domain.Entity;

namespace UFF.Infra.EntityConfig
{
    public class StoreRatingConfiguration : IEntityTypeConfiguration<StoreRating>
    {
        public void Configure(EntityTypeBuilder<StoreRating> builder)
        {            
            builder.ToTable("store_rating");
                        
            builder.HasKey(sr => sr.Id);
            builder.Property(sr => sr.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();
                        
            builder.Property(sr => sr.Score)
                .HasColumnName("score")
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            builder.Property(sr => sr.Comment)
                .HasColumnName("comment")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(sr => sr.StoreId)
                .HasColumnName("store_id")
                .IsRequired();

            builder.HasOne(sr => sr.Store)
                .WithMany(s => s.Ratings)
                .HasForeignKey(sr => sr.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(sr => sr.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.HasOne(sr => sr.User)
                .WithMany() 
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                        
            builder.Property(sr => sr.ProfessionalId)
                .HasColumnName("professional_id")
                .IsRequired(false);

            builder.HasOne(sr => sr.Professional)
                .WithMany()
                .HasForeignKey(sr => sr.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(sr => sr.StoreId).HasDatabaseName("idx_store_rating_store_id");
            builder.HasIndex(sr => sr.UserId).HasDatabaseName("idx_store_rating_user_id");
        }
    }
}
