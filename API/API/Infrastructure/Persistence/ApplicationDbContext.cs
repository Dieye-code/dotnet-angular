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
        var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

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
                }
            }
        }

        return base.SaveChangesAsync(acceptAllChangesSuccess, cancellationToken);
    }


    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SalesProducts { get; set; }
    public DbSet<User> Users { get; set; }

}
