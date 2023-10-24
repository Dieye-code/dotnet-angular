using API.Domain.Entities;
using API.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly DateTimeService _dateTime;

    public ApplicationDbContext(DbContextOptions options, DateTimeService dateTime) : base(options)
    {
        _dateTime = dateTime;
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = _dateTime.Now;
                entry.Property("UpdatedAt").CurrentValue = _dateTime.Now;
            } else
            {
                if(entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = _dateTime.Now;
                } else
                {
                    entry.State = EntityState.Modified;
                    entry.Property("DeletedAt").CurrentValue = _dateTime.Now;
                    entry.Property("IsDeleted").CurrentValue = true;
                }
            }
        }

        return base.SaveChangesAsync(acceptAllChangesSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Product>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Order>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<OrderProduct>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Sale>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<SaleProduct>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(c => !c.IsDeleted);
    }


    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SalesProducts { get; set; }
    public DbSet<User> Users { get; set; }

}
