using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Orders.Queries;

public class FindOrderQuery : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}

public class FindOrderQueryHandler : IRequestHandler<FindOrderQuery, Result<object>>
{
    private readonly IOrderRepository _orderRepository;

    public FindOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<Result<object>> Handle(FindOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.Get(request.Id, cancellationToken);
        if(order == null)
        {
            return Errors.General.NotFound(nameof(Order), request.Id);
        }
        return order.ProjectToOrderDto();
    }
}
