using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
