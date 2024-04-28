using Domain.Orders;

namespace Persistence.Repositories;

public sealed class OrderSummaryRepository(
    ApplicationDbContext context) : IOrderSummaryRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Add(OrderSummary orderSummary)
    {
        _context.OrderSummaries.Add(orderSummary);
    }
}