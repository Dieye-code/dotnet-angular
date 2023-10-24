using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<List<Order>> GetOrderDeleted(CancellationToken cancellationToken)
    {
        return _context.Orders.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
