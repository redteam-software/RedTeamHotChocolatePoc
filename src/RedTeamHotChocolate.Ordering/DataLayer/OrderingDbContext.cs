using Microsoft.EntityFrameworkCore;

namespace RedTeamHotChocolate.Ordering.DataLayer;

public class OrderingDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<LineItem> LineItems { get; set; }

    public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(e => e.Id).UseIdentityAlwaysColumn();
        modelBuilder.Entity<Order>().HasMany(e => e.Items).WithOne().HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}