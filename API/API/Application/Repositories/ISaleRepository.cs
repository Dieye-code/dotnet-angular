using API.Domain.Entities;

namespace API.Application.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<List<Sale>> GetSaleDeleted(CancellationToken cancellationToken);
}
