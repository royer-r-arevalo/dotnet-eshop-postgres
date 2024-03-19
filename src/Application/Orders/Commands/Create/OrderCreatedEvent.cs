using Domain.Orders;
using MediatR;

namespace Application.Orders.Commands.Create;

public sealed record OrderCreatedEvent(OrderId OrderId) : INotification;
