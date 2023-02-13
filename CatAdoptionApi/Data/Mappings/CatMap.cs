using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatAdoptionApi.Data.Mappings
{
    public class CatMap : IEntityTypeConfiguration<Cat>
    {
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.ToTable("cats");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            builder.HasKey("Id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Age)
                .HasColumnType("int");

            builder.Property(p => p.Breed)
                .HasMaxLength(30)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnType("char(1)")
                .IsFixedLength();

            builder.Property(p => p.Weight)
                .HasColumnType("double");
        }
    }
}
