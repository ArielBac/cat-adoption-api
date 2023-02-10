using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionApi.Data
{
    public partial class CatAdoptionContext : DbContext
    {
        public CatAdoptionContext()
        {
        }

        public CatAdoptionContext(DbContextOptions<CatAdoptionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cat> Cats { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseMySql("server=localhost;database=cat_adoption_db;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cat>(entity =>
            {
                entity.ToTable("cats");

                entity.Property(e => e.Breed).HasMaxLength(30);

                entity.Property(e => e.Color).HasMaxLength(30);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
