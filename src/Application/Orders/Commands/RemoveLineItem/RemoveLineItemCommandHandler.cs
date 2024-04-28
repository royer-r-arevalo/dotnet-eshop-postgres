using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        RemoveLineItemCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdWithLineItem(
            new OrderId(request.OrderId),
            new LineItemId(request.LineItemId));

        if(order is null)
        {
            return;
        }

        order.RemoveLineItem(new LineItemId(request.LineItemId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
