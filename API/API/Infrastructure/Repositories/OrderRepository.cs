using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async override Task<List<Order>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Orders.Include(c => c.Products).ThenInclude(c => c.Product).IgnoreAutoIncludes().ToListAsync(cancellationToken);
    }

    public async override Task<Order> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.Id == id).Include(c => c.Products).ThenInclude(c => c.Product).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Order>> GetOrderDeleted(CancellationToken cancellationToken)
    {
        return _context.Orders.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
