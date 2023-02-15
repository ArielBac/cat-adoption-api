using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatAdoptionApi.Data.Mappings
{
    public class VaccineMap : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable("vaccines");

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            builder.Property(p => p.Producer)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Applied_at)
                .IsRequired()
                .HasColumnType("datetime");

            // Da forma como estão minhas entidades, o enity constrói esse relacionamento por padrão
            // Explicitado para fins de entendimento e aprendizado
            builder.HasOne(p => p.Cat)
                .WithMany(p => p.Vaccines)
                .HasForeignKey(p => p.CatId);
        }
    }
}
