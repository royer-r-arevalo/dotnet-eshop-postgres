using Domain.Orders;
using MediatR;

namespace Application.Orders.Commands.RemoveLineItem;

public sealed record RemoveLineItemCommand(
    OrderId OrderId,
    LineItemId LineItemId) : IRequest;
