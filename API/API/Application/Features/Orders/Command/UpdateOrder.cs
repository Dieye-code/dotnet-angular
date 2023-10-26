using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using API.Infrastructure.Services;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Orders.Command;

public class UpdateOrderCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public string? SupplierName { get; set; }
    public List<ProductOrder> Products { get; set; } = new List<ProductOrder>();
}


public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<object>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DateTimeService _dateTimeService;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, DateTimeService dateTimeService)
    {
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }
    public async Task<Result<object>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.Get(request.Id, cancellationToken);
        if(order == null)
        {
            return Errors.General.NotFound(nameof(Order), request.Id);
        }
        order.SupplierName = request.SupplierName;
        _orderRepository.Update(order);

        if(request.Products.Count <= 0)
        {
            return Result.Failure("Vous devez selectionner la liste des produits à commander");
        }

        var products = order.Products.Select(c => c.Product.Id).ToList().Except(request.Products.Select(c => c.Id).ToList());
        foreach (var item in products)
        {
            _orderProductRepository.Delete(await _orderProductRepository.Get(item, cancellationToken));
        }

        foreach (var product in request.Products)
        {
            var p = await _productRepository.Get(product.Id, cancellationToken);
            if (p == null)
            {
                return Result.Failure($"Veuillez verifier la liste des produits à commander");
            }

            if (product.Price <= 0 || product.Quantity <= 0)
            {
                return Result.Failure("Veuillez verifier les produits et leur quantité à commander");
            }

            var orderProduct = (await _orderProductRepository.FindByQuery(o => o.ProductId == product.Id && o.OrderId == order.Id, cancellationToken)).FirstOrDefault();
            if(orderProduct != null)
            {
                orderProduct.Price = product.Price;
                orderProduct.Quantity = product.Quantity;
                _orderProductRepository.Update(orderProduct);
            }
        }

        await _unitOfWork.Save(cancellationToken);
        return Result.Success<object>(order.Id);
    }
}
