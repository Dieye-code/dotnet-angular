using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Product>> GetProductDeleted(CancellationToken cancellationToken)
        {
            return _context.Products.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
        }
    }
}
