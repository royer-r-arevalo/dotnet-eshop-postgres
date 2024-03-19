using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler(
    IApplicationDbContext context) : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(
        RemoveLineItemCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context
            .Orders
            .Include(order => order.LineItems.Where(lineItem => lineItem.Id == request.LineItemId))
            .SingleOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken);

        if(order is null)
        {
            return;
        }

        order.RemoveLineItem(request.LineItemId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
