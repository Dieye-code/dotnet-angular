using API.Application.Repositories;
using API.Models;
using MediatR;

namespace API.Application.Features.Products.Queries;

public class GetProductQuery : IRequest<IEnumerable<ProductDto>>
{
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll(cancellationToken);
        return products.Select(p => p.ProjectToProductDto());
    }
}
