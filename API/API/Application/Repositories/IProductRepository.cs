using API.Domain.Entities;

namespace API.Application.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetProductDeleted(CancellationToken cancellationToken);
}
