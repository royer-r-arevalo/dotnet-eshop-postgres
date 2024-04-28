using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class OrderRepository(
    ApplicationDbContext context) : IOrderRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public async Task<Order?> GetByIdWithLineItem(OrderId orderId, LineItemId lineItemId)
    {
        return await _context.Orders
            .Include(order => order.LineItems.Where(
                lineItem => lineItem.Id == lineItemId))
            .SingleOrDefaultAsync(order => order.Id == orderId);
    }
}