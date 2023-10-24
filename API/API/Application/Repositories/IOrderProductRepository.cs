using API.Domain.Entities;

namespace API.Application.Repositories;

public interface IOrderProductRepository : IBaseRepository<OrderProduct>
{
    Task<List<OrderProduct>> GetOrderProductDeleted(CancellationToken cancellationToken);
}
