using API.Domain.Entities;

namespace API.Application.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetCategoryDeleted();
}
