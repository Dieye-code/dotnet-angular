using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories;

public class SaleProductRepository : BaseRepository<SaleProduct>, ISaleProductRepository
{
    public SaleProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
