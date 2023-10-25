using API.Application.Common;
using API.Application.Repositories;
using API.Domain.Entities;
using API.Models;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public Guid CategoryId { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public double Quantity { get; set; }
    public IFormFile File { get; set; }
}

public class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Vous devez selectionner sa catégorie d'appartenance");
        RuleFor(x => x.Libelle).NotEmpty().WithMessage("Le libelle est obligatoire");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Le prix unitaire du produit est obligatoire").GreaterThanOrEqualTo(0).WithMessage("Le prix unitaire doit etre supérieur à 0");
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("La quantité en stock du produit est obligatoire").GreaterThanOrEqualTo(0).WithMessage("La quantité en stock doit etre supérieur à 0");
        RuleFor(x => x.File).NotNull().Must(x => x.ContentType.Equals("image/jpeg") || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/png"))
                .WithMessage("Le Format du fichier est invalide");
    }
}


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
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
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
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

        var category = await _categoryRepository.Get(request.CategoryId, cancellationToken);
        product.Category = category;

        _productRepository.Create(product);
        await _unitOfWork.Save(cancellationToken);
        return product.ProjectToProductDto();
    }
}