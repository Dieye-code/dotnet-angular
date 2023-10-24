using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class SaleProductRepository : BaseRepository<SaleProduct>, ISaleProductRepository
{
    public SaleProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<List<SaleProduct>> GetSaleProductDeleted(CancellationToken cancellationToken)
    {
        return _context.SalesProducts.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
