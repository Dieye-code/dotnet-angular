using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Categories.Commands;

public class DeleteCategoryCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}


public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<object>>
{
    public readonly ICategoryRepository _categoryrepository;

    public readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryrepository, IUnitOfWork unitOfWork)
    {
        _categoryrepository = categoryrepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<Result<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryrepository.Get(request.Id, cancellationToken);
        if (category == null)
        {
            return Errors.General.NotFound(nameof(Category), request.Id);
        }
        _categoryrepository.Delete(category);
        await _unitOfWork.Save(cancellationToken);

        return request.Id;
    }
}