using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using API.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Products.Commands;

public class UpdateProductCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public double Quantity { get; set; }

}


public class UpdateProductvalidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductvalidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("l'identifiant du produit est obligatoire");
        RuleFor(x => x.Id).NotEmpty().WithMessage("la categorie est obligatoire");
        RuleFor(x => x.Libelle).NotEmpty().WithMessage("Le nom du produit est obligatoire");
        RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Le prix unitaire que vous avez saisie est invalide");
        RuleFor(x => x.Quantity).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("La quantité en stock que vous avez saisie  est invalide");
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<object>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.CategoryId, cancellationToken);
        if(category == null)
        {
            return Errors.General.NotFound(nameof(Category), request.CategoryId);
        }

        var product = await _productRepository.Get(request.Id, cancellationToken);
        if(product == null)
        {
            return Errors.General.NotFound(nameof(Product), request.Id);
        }

        if(product != null)
        {
            product.Libelle = request.Libelle;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.Category = category;

            _productRepository.Update(product);
            await _unitOfWork.Save(cancellationToken);

        }

        return product.ProjectToProductDto();
    }
}