using API.Application.Repositories;
using API.Models;
using MediatR;

namespace API.Application.Features.Orders.Queries;

public class GetOrderQuey : IRequest<IEnumerable<OrderDto>>
{
}


public class GetOrderQueryHandler : IRequestHandler<GetOrderQuey, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<IEnumerable<OrderDto>> Handle(GetOrderQuey request, CancellationToken cancellationToken)
    {

        var orders = await _orderRepository.GetAll(cancellationToken);

        return orders.Select(c => c.ProjectToOrderDto());
    }
}
