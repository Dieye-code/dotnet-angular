using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<List<Sale>> GetSaleDeleted(CancellationToken cancellationToken)
    {
        return _context.Sales.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
