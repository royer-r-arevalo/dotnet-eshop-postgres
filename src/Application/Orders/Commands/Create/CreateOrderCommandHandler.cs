using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Commands.Create;

internal sealed class CreateOrderCommandHandler(
    IApplicationDbContext applicationDbContext,
    IPublisher publisher) : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly IPublisher _publisher = publisher;

    public async Task Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await _applicationDbContext.Customers.FindAsync(
            new CustomerId(request.CustomerId), cancellationToken);

        if(customer == null)
        {
            return;
        }

        Order order = Order.Create(customer.Id);
        _applicationDbContext.Orders.Add(order);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(new OrderCreatedEvent(order.Id), cancellationToken);
    }
}
