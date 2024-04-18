﻿using Domain.Orders;
using MediatR;
using Rebus.Bus;

namespace Application.Orders.Commands.Create;

internal sealed class OrderCreatedDomainEventHandler(
    IBus bus) : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        OrderCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        await _bus.Send(new OrderCreatedEvent(notification.OrderId.Value));
    }
}