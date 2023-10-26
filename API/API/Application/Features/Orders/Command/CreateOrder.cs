using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Services;
using API.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace API.Application.Features.Orders.Command;

public class ProductOrder
{
    public Guid Id { get; set; }
    public int Price { get; set; }
    public double Quantity { get; set; }
}

public class CreateOrderCommand : IRequest<Result<object>>
{
    public string SupplierName { get; set; }
    public List<ProductOrder> Products { get; set; } = new List<ProductOrder>();

}

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.SupplierName).NotEmpty().WithMessage("Le nom du fournisseur est obligatoire");
        RuleFor(x => x.Products).NotEmpty().WithMessage("Vous devez selectionner la liste des produits à commander");
    }
}


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<object>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DateTimeService _dateTimeService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, DateTimeService dateTimeService)
    {
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result<object>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var order = new Order() { SupplierName = request.SupplierName, Date = _dateTimeService.Now };
        _orderRepository.Create(order);

        foreach (var product in request.Products)
        {
            var p = await _productRepository.Get(product.Id , cancellationToken);
            if(p == null)
            {
                return Result.Failure($"Veuillez verifier la liste des produits à commander");
            }

            var orderProduct = new OrderProduct()
            {
                Order = order,
                Product = p,
                Price = p.Price,
                Quantity = p.Quantity,
            };
            _orderProductRepository.Create(orderProduct);
        }

        await _unitOfWork.Save(cancellationToken);
        return Result.Success<object>(order.Id);
    }
}
