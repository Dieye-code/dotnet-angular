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

    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }


    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SalesProducts { get; set; }
    public DbSet<User> Users { get; set; }

}
