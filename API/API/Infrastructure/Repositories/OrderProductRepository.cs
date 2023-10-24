using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories;

public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
