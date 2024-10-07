using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uff.Domain.Entity;

namespace uff.Infra.EntityConfig
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder
             .ToTable("Store")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
             .Property(c => c.LastUpdate)
             .HasColumnName("LastUpdate")
             .IsRequired();

            builder
            .Property(c => c.Description)
            .HasColumnName("Description");          

            builder
            .Property(c => c.Phone)
            .HasColumnName("Phone");

            builder
            .Property(c => c.Address)
            .HasColumnName("Address");

            builder
            .Property(c => c.Number)
            .HasColumnName("Number");

            builder
            .Property(c => c.City)
            .HasColumnName("City");

            builder
            .Property(c => c.Status)
            .HasColumnName("Status");         

            builder
            .Property(c => c.Cnpj)
            .HasColumnName("Cnpj");

            builder
            .HasOne(s => s.Owner) // Usar a propriedade Owner
            .WithMany(u => u.Stores) // Um usuário pode ter várias lojas
            .HasForeignKey(s => s.OwnerId) // Relacionamento via OwnerId
            .OnDelete(DeleteBehavior.Cascade); // Deletar a loja ao deletar o User
        }
    }
}
