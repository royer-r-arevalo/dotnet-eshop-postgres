using Domain.Orders;
using MediatR;

namespace Application.Orders.Commands.RemoveLineItem;

public sealed record RemoveLineItemCommand(
    Guid OrderId,
    Guid LineItemId) : IRequest;
