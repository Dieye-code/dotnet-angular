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

        public override async Task<List<Product>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Products.Include("Category").ToListAsync(cancellationToken);
        }

        public Task<List<Product>> GetProductDeleted(CancellationToken cancellationToken)
        {
            return _context.Products.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
        }

        public async override Task<Product> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

    }
}
