using Microsoft.EntityFrameworkCore;
using RedTeamHotChocolate.Products.Types;

namespace RedTeamHotChocolate.Products.DataLayer;

public class ProductsDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(e => e.Id).UseIdentityAlwaysColumn();

        base.OnModelCreating(modelBuilder);
    }

}


