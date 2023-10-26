using API.Application.Repositories;
using API.Models;
using MediatR;

namespace API.Application.Features.Products.Queries;

public class FindProductQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}


public class FindProductQueryHandler : IRequestHandler<FindProductQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public FindProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(FindProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        return product?.ProjectToProductDto();
    }
}