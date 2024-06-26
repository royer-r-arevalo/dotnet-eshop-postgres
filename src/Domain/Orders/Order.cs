﻿using Domain.Customers;
using Domain.Primitives;
using Domain.Products;

namespace Domain.Orders;

public sealed class Order : Entity
{
    private readonly List<LineItem> _lineItems = [];

    private Order()
    {
    }

    public OrderId Id { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public IReadOnlyCollection<LineItem> LineItems => _lineItems.AsReadOnly();

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId
        };
        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id));

        return order;
    }

    public void Add(ProductId productId, Money price)
    {
        var lineItem = new LineItem(
            new LineItemId(Guid.NewGuid()),
            Id,
            productId,
            price);

        _lineItems.Add(lineItem);
    }

    public void RemoveLineItem(LineItemId lineItemId, IOrderRepository orderRepository)
    {
        if (orderRepository.HasOneLineItem(this))
        {
            return;
        }

        var lineItem = _lineItems.FirstOrDefault(x => x.Id == lineItemId);

        if(lineItem is null)
        {
            return;
        }

        _lineItems.Remove(lineItem);
        Raise(new LineItemRemovedDomainEvent(Guid.NewGuid(), Id, lineItem.Id));
    }
}