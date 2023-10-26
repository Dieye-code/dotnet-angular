using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Products.Commands;

public class DeleteProductCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<object>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.Get(request.Id, cancellationToken);
            if (product == null)
            {
                return Errors.General.NotFound(nameof(Product), request.Id);
            }
            _productRepository.Delete(product);
            await _unitOfWork.Save(cancellationToken);
            return request.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
