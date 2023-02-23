using CatAdoptionApi.Data.Mappings;
using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionApi.Data;

public partial class CatAdoptionContext : DbContext
{
    public CatAdoptionContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public CatAdoptionContext(DbContextOptions<CatAdoptionContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatMap());
        modelBuilder.ApplyConfiguration(new VaccineMap());
    }

    public virtual DbSet<Cat> Cats { get; set; } = null!;
    public virtual DbSet<Vaccine> Vaccines { get; set; } = null!;
}
