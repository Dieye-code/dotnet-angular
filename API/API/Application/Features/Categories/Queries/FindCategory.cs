using API.Application.Exceptions;
using API.Application.Repositories;
using API.Models;
using MediatR;

namespace API.Application.Features.Categories.Queries;

public class FindCategoryQuery : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
}

public class FindcategoryQueryHandler : IRequestHandler<FindCategoryQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public FindcategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryDto> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        if(category == null)
        {
            throw new NotFoundException($"la category n'existe pas dans la base de données");
        }
        return category.ProjectToCategoryDto();
    }
}
