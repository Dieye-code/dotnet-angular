using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using API.Models;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Products.Queries;

public class FindProductQuery : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}


public class FindProductQueryHandler : IRequestHandler<FindProductQuery, Result<object>>
{
    private readonly IProductRepository _productRepository;

    public FindProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<object>> Handle(FindProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        if(product == null)
        {
            return Errors.General.NotFound(nameof(Product), request.Id);
        }
        return Result.Success(product?.ProjectToProductDto());
    }
}