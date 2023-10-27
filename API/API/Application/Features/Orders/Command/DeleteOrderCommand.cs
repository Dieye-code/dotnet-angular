using API.Application.Repositories;
using API.Domain.Common;
using API.Domain.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace API.Application.Features.Orders.Command;

public class DeleteOrderCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<object>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<object>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.Get(request.Id, cancellationToken);

        if(order == null)
        {
            return Errors.General.NotFound(nameof(Order), request.Id);
        }
        try
        {
            _orderRepository.Delete(order);
            await _unitOfWork.Save(cancellationToken);
            return Result.Success(order);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
