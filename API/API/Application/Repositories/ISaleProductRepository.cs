using API.Domain.Entities;

namespace API.Application.Repositories;

public interface ISaleProductRepository : IBaseRepository<SaleProduct>
{
    Task<List<SaleProduct>> GetSaleProductDeleted(CancellationToken cancellationToken);
}
