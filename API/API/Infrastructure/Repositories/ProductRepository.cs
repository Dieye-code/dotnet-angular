using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
