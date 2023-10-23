using API.Application.Repositories;
using MediatR;

namespace API.Application.Features.Categories.Commands;

public class DeleteCategoryCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}


public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Guid>
{
    public readonly ICategoryRepository _categoryrepository;

    public readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryrepository, IUnitOfWork unitOfWork)
    {
        _categoryrepository = categoryrepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryrepository.Get(request.Id, cancellationToken);
        if(category != null)
        {
            _categoryrepository.Delete(category);
            await _unitOfWork.Save(cancellationToken);
        }
        return request.Id;
    }
}