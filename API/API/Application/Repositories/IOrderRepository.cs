using API.Domain.Entities;

namespace API.Application.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> GetOrderDeleted(CancellationToken cancellationToken);
}
