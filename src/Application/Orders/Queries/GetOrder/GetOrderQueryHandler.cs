using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrder;

internal sealed class GetOrderQueryHandler(
    IApplicationDbContext context) : IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<OrderResponse> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var orderSummaries = await _context
            .Database
            .SqlQuery<OrderSummary>(@$"
                SELECT o.Id AS OrderId, o.CustomerId, li.Id AS LineItemId, li.Price_Amount AS LineItemPrice
                FROM Orders AS o
                JOIN LineItems AS li ON li.OrderId = o.Id
                WHERE o.Id = {request.OrderId}")
            .ToListAsync(cancellationToken);

        var orderResponse = orderSummaries
            .GroupBy(order => order.OrderId)
            .Select(grp => new OrderResponse(
                grp.Key,
                grp.First().CustomerId,
                grp.Select(summary => new LineItemResponse(summary.LineItemId, summary.LineItemPrice)).ToList()))
            .Single();

        return orderResponse;
    }

    private sealed record OrderSummary(
        Guid OrderId,
        Guid CustomerId,
        Guid LineItemId,
        decimal LineItemPrice);

    //public async Task<OrderResponse> Handle(
    //    GetOrderQuery request,
    //    CancellationToken cancellationToken)
    //{
    //    var order = await _context
    //        .Orders
    //        .Where(order => order.Id == new OrderId(request.OrderId))
    //        .Select(order => new OrderResponse(
    //            order.Id.Value,
    //            order.CustomerId.Value,
    //            order.LineItems
    //                .Select(lineItem => new LineItemResponse(lineItem.Id.Value, lineItem.Price.Amount))
    //                .ToList()))
    //        .SingleAsync(cancellationToken);

    //    return order;
    //}

    //public async Task<OrderResponse> Handle(
    //    GetOrderQuery request,
    //    CancellationToken cancellationToken)
    //{
    //    var order = await _context
    //        .Orders
    //        .Include(order => order.LineItems)
    //        .SingleAsync(order => order.Id == new OrderId(request.OrderId), cancellationToken);

    //    var orderResponse = new OrderResponse(
    //        order.Id.Value,
    //        order.CustomerId.Value,
    //        order.LineItems
    //            .Select(lineItem => new LineItemResponse(lineItem.Id.Value, lineItem.Price.Amount))
    //            .ToList());

    //    return orderResponse;
    //}
}
