using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<List<OrderProduct>> GetOrderProductDeleted(CancellationToken cancellationToken)
    {
        return _context.OrderProducts.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
