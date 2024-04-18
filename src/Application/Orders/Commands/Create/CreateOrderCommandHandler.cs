using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;
using Rebus.Bus;

namespace Application.Orders.Commands.Create;

internal sealed class CreateOrderCommandHandler(
    IApplicationDbContext applicationDbContext) : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await _applicationDbContext.Customers.FindAsync(
            new CustomerId(request.CustomerId), cancellationToken);

        if (customer == null)
        {
            return;
        }

        Order order = Order.Create(customer.Id);
        _applicationDbContext.Orders.Add(order);
        _applicationDbContext.OrderSummaries.Add(new OrderSummary(
            order.Id.Value,
            order.CustomerId.Value,
            0));
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        //await _bus.Send(new OrderCreatedEvent(order.Id.Value));
    }
}

//internal sealed class CreateOrderCommandHandler(
//    IRepository<Customer> customerRepository,
//    IRepository<Order> orderRepository,
//    IPublisher publisher) : IRequestHandler<CreateOrderCommand>
//{
//    private readonly IRepository<Customer> _customerRepository = customerRepository;
//    private readonly IRepository<Order> _orderRepository = orderRepository;
//    private readonly IPublisher _publisher = publisher;

//    public async Task Handle(
//      CreateOrderCommand request,
//      CancellationToken cancellationToken)
//    {
//        Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId);

//        if (customer is null)
//        {
//            return;
//        }

//        Order order = Order.Create(customer.Id);
//        _orderRepository.Insert(order);
//        await _orderRepository.SaveChangesAsync();
//        await _publisher.Publish(new OrderCreatedEvent(order.Id), cancellationToken);
//    }
//}
