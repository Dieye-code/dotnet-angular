
using API.Application.Repositories;
using API.Models;
using MediatR;

namespace API.Application.Features.Categories.Queries;

public class GetCategoryQuery : IRequest<IEnumerable<CategoryDto>>
{

}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll(cancellationToken);
        return categories.Select(category => category.ProjectToCategoryDto());
    }
}
