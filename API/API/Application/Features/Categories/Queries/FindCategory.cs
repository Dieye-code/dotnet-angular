using API.Application.Exceptions;
using API.Application.Repositories;
using API.Domain.Common;
using API.Models;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Categories.Queries;

public class FindCategoryQuery : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}

public class FindcategoryQueryHandler : IRequestHandler<FindCategoryQuery, Result<object>>
{
    private readonly ICategoryRepository _categoryRepository;

    public FindcategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<object>> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        if(category == null)
        {
            return Errors.General.NotFound(nameof(category), request.Id);
        }
        return Result.Success(category.ProjectToCategoryDto());
    }
}
