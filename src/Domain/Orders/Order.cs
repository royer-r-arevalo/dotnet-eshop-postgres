using Domain.Customers;
using Domain.Products;

namespace Domain.Orders;

public sealed class Order
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

    public void RemoveLineItem(LineItemId lineItemId)
    {
        var lineItem = _lineItems.FirstOrDefault(x => x.Id == lineItemId);
        if(lineItem is null)
        {
            return;
        }
        _lineItems.Remove(lineItem);
    }
}