using Domain.Customers;

namespace Domain.Orders;

public interface IOrderRepository
{
    void Add(Order order);

    Task<Order?> GetByIdWithLineItem(OrderId orderId, LineItemId lineItemId);

    bool HasOneLineItem(Order order);
}