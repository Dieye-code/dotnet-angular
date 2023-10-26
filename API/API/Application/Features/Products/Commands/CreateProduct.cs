using API.Application.Common;
using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using API.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<Result<object>>
{
    public Guid CategoryId { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public double Quantity { get; set; }
    public IFormFile File { get; set; }
}

public class FileValidation : AbstractValidator<IFormFile>
{
    public FileValidation()
    {
        RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(100 * 1024).WithMessage("La taille de l'image est trop grande");
        RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png")).WithMessage("Le Format du fichier est invalide");
    }
}

public class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Vous devez selectionner sa catégorie d'appartenance");
        RuleFor(x => x.Libelle).NotEmpty().WithMessage("Le libelle est obligatoire");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Le prix unitaire du produit est obligatoire").GreaterThanOrEqualTo(0).WithMessage("Le prix unitaire doit etre supérieur à 0");
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("La quantité en stock du produit est obligatoire").GreaterThanOrEqualTo(0).WithMessage("La quantité en stock doit etre supérieur à 0");
        RuleFor(x => x.File).SetValidator(new FileValidation());
    }
}


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandHandler(IProductRepository productRepository, IFileService fileService, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.CategoryId, cancellationToken);
        if(category == null)
        {
            return Errors.General.NotFound(nameof(Category), request.CategoryId);
        }
        var fileName = await _fileService.UploadFile(request.File);
        var product = new Product()
        {
            CategoryId = request.CategoryId,
            Libelle = request.Libelle,
            Description = request.Description,
            Photo = fileName,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        product.Category = category;

        _productRepository.Create(product);
        await _unitOfWork.Save(cancellationToken);
        return product.ProjectToProductDto();
    }
}