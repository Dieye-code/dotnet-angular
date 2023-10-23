using API.Application.Repositories;
using API.Domain.Entities;
using API.Models;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Categories.Commands;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Libelle { get; set; }
}

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Libelle).NotEmpty().WithMessage("Le libelle est obligatoire");
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Libelle = request.Libelle;

        _categoryRepository.Create(category);
        await _unitOfWork.Save(cancellationToken);

        return category.ProjectToCategoryDto();
    }
}
