using Application.Data;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrderSummary;

internal sealed class GetOrderSummaryQueryHandler(
    IApplicationDbContext context) : IRequestHandler<GetOrderSummaryQuery, OrderSummary?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<OrderSummary?> Handle(
        GetOrderSummaryQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(summary => summary.Id == request.OrderId, cancellationToken);
    }
}
