using Models.Applications;
using Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class BackOfficeDbContext : DbContext
{
    #region DataSets
    public virtual DbSet<Application> Applications { get; set; } = default!;

    public virtual DbSet<Product> Products { get; set; } = default!;
    public virtual DbSet<ProductTier> ProductTiers { get; set; } = default!;
    #endregion

    public BackOfficeDbContext(DbContextOptions<BackOfficeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Manually configure entity relationships

        // 2. Add any global filters
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
