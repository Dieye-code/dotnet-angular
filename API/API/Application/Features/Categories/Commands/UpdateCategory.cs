using API.Application.Repositories;
using API.Domain.Entities;
using API.Models;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Categories.Commands;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public string Libelle { get; set; }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("L'identifiant du category est obligatoire");
        RuleFor(x => x.Libelle).NotEmpty().WithMessage("Le libellé du category est obligatoire");
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    public readonly ICategoryRepository _categoryrepository;
    public readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryrepository, IUnitOfWork unitOfWork)
    {
        _categoryrepository = categoryrepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryrepository.Get(request.Id, cancellationToken);
        if(category != null)
        {
            category.Id = request.Id;
            category.Libelle = request.Libelle;
            _categoryrepository.Update(category);
            await _unitOfWork.Save(cancellationToken);
        }
        return category.ProjectToCategoryDto();
    }
}